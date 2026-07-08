@echo off
chcp 65001 >nul
title DocearReminder Publish and Run

echo ==========================================
echo   DocearReminder Publish and Run
echo ==========================================
echo.

powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0PublishAndRun.ps1" %*
set ERR=%ERRORLEVEL%

if %ERR% neq 0 (
    echo.
    echo Failed, error code: %ERR%
    pause
    exit /b %ERR%
)

exit /b 0
