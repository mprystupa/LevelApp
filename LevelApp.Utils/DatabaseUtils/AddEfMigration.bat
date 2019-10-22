@echo off
set /p name="Migration name: "
cd ../..
cd LevelApp.DAL
dotnet ef --startup-project ../LevelApp.API migrations add %name%