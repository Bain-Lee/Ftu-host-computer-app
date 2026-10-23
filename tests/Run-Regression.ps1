param
(
    [Parameter(Mandatory = $true)]
    [string]$BuildDirectory
)

$ErrorActionPreference = 'Stop'
$buildPath = (Resolve-Path -LiteralPath $BuildDirectory).Path
$applicationName = '配电终端测试软件V2.0.exe'
$applicationPath = Join-Path $buildPath $applicationName
if (-not (Test-Path -LiteralPath $applicationPath -PathType Leaf))
{
    throw "Build output is missing: $applicationPath"
}

$testDirectory = Join-Path $env:TEMP ('DtuTestManager-regression-' + [Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $testDirectory | Out-Null
Copy-Item -LiteralPath $applicationPath -Destination $testDirectory
Get-ChildItem -LiteralPath $buildPath -Filter '*.dll' -File | Copy-Item -Destination $testDirectory
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe'
$testExecutable = Join-Path $testDirectory 'BaselineRegression.exe'
& $compiler /nologo /target:exe /platform:x86 /warn:4 /warnaserror+ `
    "/reference:$applicationPath" "/reference:$buildPath\System.Data.SQLite.dll" `
    "/out:$testExecutable" (Join-Path $PSScriptRoot 'BaselineRegression.cs')
if ($LASTEXITCODE -ne 0)
{
    throw 'Regression test compilation failed.'
}

Push-Location -LiteralPath $testDirectory
try
{
    & $testExecutable
    $testExitCode = $LASTEXITCODE
}
finally
{
    Pop-Location
}
Write-Output "Regression output: $testDirectory"
if ($testExitCode -ne 0)
{
    throw 'Baseline regression checks failed.'
}
