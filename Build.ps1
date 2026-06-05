# DocearReminder Build Script
# Target Directory: D:\Dropbox\Software\DocearReminder

# Set console encoding to UTF-8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$OutputEncoding = [System.Text.Encoding]::UTF8

Write-Host "==========================================" -ForegroundColor Cyan
Write-Host "  DocearReminder Build Script" -ForegroundColor Cyan
Write-Host "==========================================" -ForegroundColor Cyan
Write-Host ""

# Set path variables
$solutionPath = Join-Path $PSScriptRoot "DocearReminder.sln"
$targetDir = "D:\Dropbox\Software\DocearReminder"
$demoDir = Join-Path $PSScriptRoot "Demo"
$projectDir = Join-Path $PSScriptRoot "DocearReminder"

Write-Host "Solution: $solutionPath" -ForegroundColor Yellow
Write-Host "Target: $targetDir" -ForegroundColor Yellow
Write-Host ""

# Check for MSBuild
Write-Host "Finding MSBuild..." -ForegroundColor Cyan
$msbuildPaths = @(
    "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe",
    "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe",
    "C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
)

$msbuildPath = $null
foreach ($path in $msbuildPaths) {
    if (Test-Path $path) {
        $msbuildPath = $path
        break
    }
}

if (-not $msbuildPath) {
    Write-Host "ERROR: MSBuild not found. Please install Visual Studio or .NET Framework" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host "Found MSBuild: $msbuildPath" -ForegroundColor Green
Write-Host ""

# Ensure target directory exists
Write-Host "Checking target directory..." -ForegroundColor Cyan
if (-not (Test-Path $targetDir)) {
    New-Item -ItemType Directory -Path $targetDir -Force | Out-Null
    Write-Host "Target directory created" -ForegroundColor Green
} else {
    Write-Host "Target directory exists, will overwrite files" -ForegroundColor Yellow
}
Write-Host ""

# Build project
Write-Host "Building project (Release|Any CPU)..." -ForegroundColor Cyan
$buildArgs = @(
    $solutionPath,
    "/t:Rebuild",
    "/p:Configuration=Release",
    "/p:Platform=Any CPU",
    "/v:minimal",
    "/m"
)

$buildResult = & $msbuildPath $buildArgs 2>&1
$buildExitCode = $LASTEXITCODE

if ($buildExitCode -eq 0) {
    Write-Host "Build successful!" -ForegroundColor Green
} else {
    Write-Host "Build failed!" -ForegroundColor Red
    Write-Host ""
    Write-Host "Build output:" -ForegroundColor Yellow
    Write-Host $buildResult
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit $buildExitCode
}

Write-Host ""

# Copy necessary resource files
Write-Host "Copying resource files..." -ForegroundColor Cyan

# Create subdirectories
$subDirs = @("Skins", "Sounds", "Demo")
foreach ($dir in $subDirs) {
    $destPath = Join-Path $targetDir $dir
    if (-not (Test-Path $destPath)) {
        New-Item -ItemType Directory -Path $destPath -Force | Out-Null
    }
}

# Copy resource files from Demo directory
$copySources = @(
    @{ Source = "Skins"; Dest = "Skins" },
    @{ Source = "Sounds"; Dest = "Sounds" },
    @{ Source = "Demo"; Dest = "Demo" }
)

foreach ($source in $copySources) {
    $sourcePath = Join-Path $demoDir $source.Source
    $destPath = Join-Path $targetDir $source.Dest
    
    if (Test-Path $sourcePath) {
        Write-Host "Copying $($source.Source) to $($source.Dest)..." -ForegroundColor Yellow
        Copy-Item -Path "$sourcePath\*" -Destination $destPath -Recurse -Force
    }
}

Write-Host ""

# Check if successful
Write-Host "Checking generated files..." -ForegroundColor Cyan
$exePath = Join-Path $targetDir "DocearReminder.exe"

if (Test-Path $exePath) {
    Write-Host ""
    Write-Host "==========================================" -ForegroundColor Green
    Write-Host "  Build Successful!" -ForegroundColor Green
    Write-Host "==========================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "Output Directory: $targetDir" -ForegroundColor Cyan
    Write-Host "Main Program: $exePath" -ForegroundColor Cyan
    Write-Host ""
    
    # Show file statistics
    $files = Get-ChildItem $targetDir -File
    $totalSize = ($files | Measure-Object -Property Length -Sum).Sum
    $totalSizeMB = [math]::Round($totalSize / 1MB, 2)
    Write-Host "File Count: $($files.Count)" -ForegroundColor Yellow
    Write-Host "Total Size: ${totalSizeMB} MB" -ForegroundColor Yellow
} else {
    Write-Host "ERROR: Main program file not found" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host ""
Write-Host "Press Enter to exit..."
Read-Host
