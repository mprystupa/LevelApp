@echo off
cd ../..
cd LevelApp.DAL
dotnet ef --startup-project ../LevelApp.API database update

cd ../LevelApp.Utils/DatabaseUtils