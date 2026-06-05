@echo off
chcp 65001 >nul
title DocearReminder Build Script

echo ==========================================
echo   DocearReminder Build Script
echo ==========================================
echo.

:: Check PowerShell version
for /f "tokens=3" %%i in ('powershell -Command "$PSVersionTable.PSVersion.Major"') do set PSVersion=%%i
if %PSVersion% LSS 3 (
    echo ERROR: PowerShell 3.0 or later is required
    pause
    exit /b 1
)

:: Run PowerShell script
powershell -ExecutionPolicy Bypass -File "%~dp0Build.ps1"

:: Pause to see results
if %errorlevel% neq 0 (
    echo.
    echo Build failed, error code: %errorlevel%
    pause
)
