dotnet test LevelApp.BLL.Tests\LevelApp.BLL.Tests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
dotnet test LevelApp.DAL.Tests\LevelApp.DAL.Tests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
dotnet sonarscanner begin /k:"levelapp" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="c57a119269d225338818b4b2b262c88f5012e9fd"
cd ..
dotnet build LevelApp.sln
dotnet sonarscanner end /d:sonar.login="c57a119269d225338818b4b2b262c88f5012e9fd"

set /p DUMMY=Hit ENTER to continue...