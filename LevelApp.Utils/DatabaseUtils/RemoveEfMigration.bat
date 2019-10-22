@echo off
cd ../..
cd LevelApp.DAL
dotnet ef --startup-project ../LevelApp.API migrations remove