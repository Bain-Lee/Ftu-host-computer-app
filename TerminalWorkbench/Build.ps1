param
(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Release',
    [switch]$Publish,
    [switch]$Verify
)

$ErrorActionPreference = 'Stop'
$sdkVersion = '10.0.402'
$localSdk = Join-Path $PSScriptRoot '.tools\dotnet'
$dotnetExecutable = Join-Path $localSdk 'dotnet.exe'
if (-not (Test-Path -LiteralPath $dotnetExecutable -PathType Leaf))
{
    New-Item -ItemType Directory -Path $localSdk -Force | Out-Null
    $sdkArchive = Join-Path $env:TEMP "terminal-workbench-dotnet-sdk-$sdkVersion-win-x64.zip"
    $sdkUrl = "https://builds.dotnet.microsoft.com/dotnet/Sdk/$sdkVersion/dotnet-sdk-$sdkVersion-win-x64.zip"
    Write-Output "Downloading project-local .NET SDK $sdkVersion."
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
        & $regressionExe --workflow --templates $templates
        if ($LASTEXITCODE -ne 0)
        {
            throw 'Workflow regression failed.'
        }
    }
    if ($Publish)
    {
        $publishDirectory = Join-Path $PSScriptRoot '.artifacts\publish\win-x86'
        & $dotnetExecutable publish TerminalWorkbench.App\TerminalWorkbench.App.csproj --configuration $Configuration --self-contained true --runtime win-x86 --output $publishDirectory --verbosity minimal
        if ($LASTEXITCODE -ne 0)
        {
            throw 'Publish failed.'
        }
        Write-Output "Application: $publishDirectory\TerminalWorkbench.exe"
    }
}
finally
{
    Pop-Location
}
