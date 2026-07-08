# DocearReminder: build, deploy to test folder, and launch.
# Re-run: stops any running instance, overwrites deploy files (incl. config.ini), then starts again.

param(
    [string]$DeployDir = "E:\Temp\DcoearReminder",
    [string]$BuildDir = "D:\Dropbox\Software\DocearReminder",
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release",
    [switch]$SkipBuild
)

[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$OutputEncoding = [System.Text.Encoding]::UTF8

$ErrorActionPreference = "Stop"
$RepoRoot = $PSScriptRoot
$SolutionPath = Join-Path $RepoRoot "DocearReminder.sln"
$DemoDir = Join-Path $RepoRoot "Demo"
$ExeName = "DocearReminder.exe"
$DeployExe = Join-Path $DeployDir $ExeName

# User/runtime data under build output — do not mirror into test deploy folder.
$RobocopyExcludeDirs = @(
    "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023", "2024", "2025", "2026",
    "backup", "log", "reminderjson", "TimeBlock", ".git", ".vs", "obj", "bin"
)

function Write-Step([string]$Message) {
    Write-Host ""
    Write-Host ">> $Message" -ForegroundColor Cyan
}

function Find-MsBuild {
    $paths = @(
        "${env:ProgramFiles}\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe",
        "${env:ProgramFiles}\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe",
        "${env:ProgramFiles}\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe",
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe",
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe",
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MSBuild.exe",
        "${env:Windir}\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"
    )
    foreach ($path in $paths) {
        if (Test-Path $path) { return $path }
    }
    return $null
}

function Stop-DocearReminderApp {
    Write-Step "Stopping running DocearReminder..."
    $processes = Get-CimInstance Win32_Process -Filter "Name='$ExeName'" -ErrorAction SilentlyContinue
    if (-not $processes) {
        Write-Host "No running instance found." -ForegroundColor DarkGray
        return
    }

    foreach ($proc in $processes) {
        Write-Host "Stopping PID $($proc.ProcessId): $($proc.ExecutablePath)" -ForegroundColor Yellow
        Stop-Process -Id $proc.ProcessId -Force -ErrorAction SilentlyContinue
    }

    $deadline = (Get-Date).AddSeconds(20)
    while ((Get-Process -Name "DocearReminder" -ErrorAction SilentlyContinue) -and (Get-Date) -lt $deadline) {
        Start-Sleep -Milliseconds 300
    }

    if (Get-Process -Name "DocearReminder" -ErrorAction SilentlyContinue) {
        throw "Failed to stop DocearReminder.exe. Close it manually and run the script again."
    }
    Write-Host "Stopped." -ForegroundColor Green
}

function Invoke-Build {
    param([string]$MsBuildPath)

    Write-Step "Building ($Configuration|Any CPU) -> $BuildDir"
    if (-not (Test-Path $BuildDir)) {
        New-Item -ItemType Directory -Path $BuildDir -Force | Out-Null
    }

    $normalizedBuildDir = $BuildDir.TrimEnd('\') + '\'
    $args = @(
        $SolutionPath,
        "/t:Rebuild",
        "/p:Configuration=$Configuration",
        "/p:Platform=Any CPU",
        "/p:OutputPath=$normalizedBuildDir",
        "/v:minimal",
        "/m"
    )

    & $MsBuildPath @args
    if ($LASTEXITCODE -ne 0) {
        throw "MSBuild failed with exit code $LASTEXITCODE"
    }

    $buildExe = Join-Path $BuildDir $ExeName
    if (-not (Test-Path $buildExe)) {
        throw "Build finished but executable not found: $buildExe"
    }
    Write-Host "Build OK: $buildExe" -ForegroundColor Green
}

function Copy-ConfigIni {
    param([string]$TargetDir)

    # Prefer production config in build output; Demo config is fallback only.
    $configCandidates = @(
        (Join-Path $BuildDir "config.ini"),
        (Join-Path $DemoDir "config.ini")
    )
    foreach ($configPath in $configCandidates) {
        if (Test-Path $configPath) {
            $dest = Join-Path $TargetDir "config.ini"
            if ((Resolve-Path $configPath).Path -eq (Resolve-Path $dest -ErrorAction SilentlyContinue).Path) {
                Write-Host "config.ini unchanged: $configPath" -ForegroundColor DarkGray
                return
            }
            Copy-Item -Path $configPath -Destination $dest -Force
            Write-Host "config.ini <= $configPath" -ForegroundColor Green
            return
        }
    }
    Write-Host "Warning: config.ini not found in build output or Demo." -ForegroundColor Yellow
}

function Copy-DemoResources {
    param(
        [string]$TargetDir,
        [switch]$IncludeConfig
    )

    Write-Step "Copying Demo resources (Skins / Sounds / Demo)..."
    $resourceDirs = @("Skins", "Sounds", "Demo")
    foreach ($name in $resourceDirs) {
        $source = Join-Path $DemoDir $name
        if (-not (Test-Path $source)) { continue }
        $dest = Join-Path $TargetDir $name
        if (-not (Test-Path $dest)) {
            New-Item -ItemType Directory -Path $dest -Force | Out-Null
        }
        Copy-Item -Path (Join-Path $source "*") -Destination $dest -Recurse -Force
    }

    if ($IncludeConfig) {
        Copy-ConfigIni -TargetDir $TargetDir
    }
}

function Sync-DeployDirectory {
    Write-Step "Deploying to $DeployDir (overwrite existing files)..."
    if (-not (Test-Path $DeployDir)) {
        New-Item -ItemType Directory -Path $DeployDir -Force | Out-Null
    }

    $xdArgs = @()
    foreach ($dir in $RobocopyExcludeDirs) {
        $xdArgs += "/XD"
        $xdArgs += $dir
    }

    # /MIR: make deploy match build output (minus excluded dirs).
    # /IS /IT: overwrite even if timestamps look identical.
    $robocopyArgs = @(
        $BuildDir,
        $DeployDir,
        "/MIR",
        "/IS",
        "/IT",
        "/R:2",
        "/W:2",
        "/NFL",
        "/NDL",
        "/NJH",
        "/NJS",
        "/NP"
    ) + $xdArgs

    & robocopy @robocopyArgs | Out-Null
    $rc = $LASTEXITCODE
    # Robocopy: 0-7 = success; >=8 = failure
    if ($rc -ge 8) {
        throw "Robocopy failed with exit code $rc"
    }

    Copy-DemoResources -TargetDir $DeployDir -IncludeConfig
    Write-Host "Deploy OK." -ForegroundColor Green
}

function Start-DocearReminderApp {
    Write-Step "Starting DocearReminder..."
    if (-not (Test-Path $DeployExe)) {
        throw "Executable not found: $DeployExe"
    }

    $proc = Start-Process -FilePath $DeployExe -WorkingDirectory $DeployDir -PassThru
    Start-Sleep -Seconds 4
    $proc.Refresh()
    if ($proc.HasExited) {
        throw "DocearReminder exited immediately (code $($proc.ExitCode)). Check error.txt in deploy folder."
    }
    Write-Host "Started: $DeployExe (PID $($proc.Id))" -ForegroundColor Green
    $title = (Get-Process -Id $proc.Id -ErrorAction SilentlyContinue).MainWindowTitle
    if ($title) {
        Write-Host "Window: $title" -ForegroundColor Green
    }
    else {
        Write-Host "Window is hidden (ShowInTaskbar=false). Press Shift+Space or check system tray." -ForegroundColor Yellow
    }
}

Write-Host "==========================================" -ForegroundColor Cyan
Write-Host "  DocearReminder Publish & Run" -ForegroundColor Cyan
Write-Host "==========================================" -ForegroundColor Cyan
Write-Host "Deploy: $DeployDir"
Write-Host "Build:  $BuildDir"

try {
    Stop-DocearReminderApp

    if (-not $SkipBuild) {
        $msbuild = Find-MsBuild
        if (-not $msbuild) {
            throw "MSBuild not found. Install Visual Studio or .NET Framework SDK."
        }
        Write-Host "MSBuild: $msbuild" -ForegroundColor DarkGray
        Invoke-Build -MsBuildPath $msbuild
        Copy-DemoResources -TargetDir $BuildDir
        Copy-ConfigIni -TargetDir $BuildDir
    }
    else {
        Write-Host "SkipBuild: using existing build output." -ForegroundColor Yellow
        if (-not (Test-Path (Join-Path $BuildDir $ExeName))) {
            throw "SkipBuild set but $ExeName not found in $BuildDir"
        }
    }

    Sync-DeployDirectory
    Start-DocearReminderApp

    Write-Host ""
    Write-Host "Done." -ForegroundColor Green
}
catch {
    Write-Host ""
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}
