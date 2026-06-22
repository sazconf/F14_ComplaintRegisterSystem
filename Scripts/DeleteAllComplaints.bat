@echo off
title Delete All Complaints

echo.
echo ====================================
echo   DELETE ALL COMPLAINT RECORDS
echo ====================================
echo.

set /p confirm=Type YES to continue: 

if /I not "%confirm%"=="YES" (
    echo Operation cancelled.
    pause
    exit /b
)

sqlcmd -S "(localdb)\MSSQLLocalDB2022" -d ComplaintDB -Q "DELETE FROM complaints"

echo.
echo All complaint records deleted successfully.
echo.

pause