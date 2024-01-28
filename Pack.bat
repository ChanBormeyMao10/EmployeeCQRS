rem Pack
.nuget\NuGet.exe Pack Employee2\Employee2.csproj -Symbols -OutputDirectory Packages
.nuget\NuGet.exe Pack Employee2.ReadModels\Employee2.ReadModels.csproj -OutputDirectory Packages
.nuget\NuGet.exe Pack Employee2.CommandHandler\Employee2.CommandHandler.csproj -OutputDirectory Packages
pause