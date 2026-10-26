param
(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Release',
    [switch]$Publish,
    [switch]$Verify
)

$ErrorActionPreference = 'Stop'
$sdkVersion = '10.0.402'
$localSdk = Join-Path $env:LOCALAPPDATA "TerminalWorkbench.BuildTools\dotnet-$sdkVersion"
$dotnetExecutable = Join-Path $localSdk 'dotnet.exe'
if (-not (Test-Path -LiteralPath $dotnetExecutable -PathType Leaf))
{
    New-Item -ItemType Directory -Path $localSdk -Force | Out-Null
    $sdkArchive = Join-Path $env:TEMP "terminal-workbench-dotnet-sdk-$sdkVersion-win-x64.zip"
    $sdkUrl = "https://builds.dotnet.microsoft.com/dotnet/Sdk/$sdkVersion/dotnet-sdk-$sdkVersion-win-x64.zip"
    Write-Output "Downloading private .NET SDK $sdkVersion to the user build cache."
    Invoke-WebRequest -Uri $sdkUrl -OutFile $sdkArchive -UseBasicParsing
    Expand-Archive -LiteralPath $sdkArchive -DestinationPath $localSdk -Force
}

$env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'
$env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
$env:DOTNET_GENERATE_ASPNET_CERTIFICATE = 'false'
$env:MSBuildEnableWorkloadResolver = 'false'
Push-Location -LiteralPath $PSScriptRoot
try
{
    & $dotnetExecutable build TerminalWorkbench.sln --configuration $Configuration --verbosity minimal
    if ($LASTEXITCODE -ne 0)
    {
        throw 'Build failed.'
    }
    if ($Verify)
    {
        $regressionExe = Join-Path $PSScriptRoot "TerminalWorkbench.Regression\bin\$Configuration\win-x86\TerminalWorkbench.Regression.exe"
        $templates = Join-Path $PSScriptRoot '..\DtuTestManager\bin\Debug\DefaultDevModule'
        & $regressionExe --templates $templates
        if ($LASTEXITCODE -ne 0)
        {
            throw 'Offline regression failed.'
        }
        & $regressionExe --workflow
        if ($LASTEXITCODE -ne 0)
        {
            throw 'Workflow regression failed.'
        }
    }
    if ($Publish)
    {
        $publishDirectory = Join-Path $PSScriptRoot '.artifacts\publish\win-x86'
        $stagingDirectory = Join-Path $PSScriptRoot ('.artifacts\publish\staging-' + [Guid]::NewGuid().ToString('N'))
        & $dotnetExecutable publish TerminalWorkbench.App\TerminalWorkbench.App.csproj --configuration $Configuration /p:PublishProfile=Standalone --output $stagingDirectory --verbosity minimal
        if ($LASTEXITCODE -ne 0)
        {
            throw 'Publish failed.'
        }
        $stagedFiles = @(Get-ChildItem -LiteralPath $stagingDirectory -File -Recurse)
        if ($stagedFiles.Count -ne 1 -or $stagedFiles[0].Name -ne 'TerminalWorkbench.exe')
        {
            throw 'The standalone package must contain exactly one executable.'
        }
        $expectedPublishRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '.artifacts\publish'))
        $resolvedPublishDirectory = [System.IO.Path]::GetFullPath($publishDirectory)
        $resolvedStagingDirectory = [System.IO.Path]::GetFullPath($stagingDirectory)
        if ($resolvedPublishDirectory -ne (Join-Path $expectedPublishRoot 'win-x86') -or
            -not $resolvedStagingDirectory.StartsWith($expectedPublishRoot + [System.IO.Path]::DirectorySeparatorChar, [System.StringComparison]::OrdinalIgnoreCase))
        {
            throw 'Unexpected publish directory.'
        }
        if (Test-Path -LiteralPath $publishDirectory)
        {
            $userData = @(Get-ChildItem -LiteralPath $publishDirectory -File -Recurse | Where-Object {
                $_.Extension -in @('.ini', '.mdb', '.db', '.pfx', '.pem', '.key', '.cer', '.crt') -or $_.Name -in @('TZhuZhanDll.dll', 'HsmPriDll.dll')
            })
            if ($userData.Count -ne 0)
            {
                throw 'The old publish directory contains user data; preserve it before publishing.'
            }
            Remove-Item -LiteralPath $resolvedPublishDirectory -Recurse -Force
        }
        Move-Item -LiteralPath $resolvedStagingDirectory -Destination $resolvedPublishDirectory
        Write-Output "Application: $publishDirectory\TerminalWorkbench.exe"
    }
}
finally
{
    Pop-Location
}
