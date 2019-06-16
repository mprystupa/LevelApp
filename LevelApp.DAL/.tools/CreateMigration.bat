@echo off
set /p name="Enter migration name: "
cd ..
dotnet ef migrations add %name%