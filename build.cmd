@set FAKE_DETAILED_ERRORS=true
@SET scriptPath=%~dp0
dotnet new tool-manifest --force
@IF %ERRORLEVEL% NEQ 0 call :error "Tool manifest failed"
dotnet tool install fake-cli --configfile %scriptPath%\deploy\NuGet-OfficialOnly.config
@IF %ERRORLEVEL% NEQ 0 call :error "Fake-cli installation failed"
dotnet tool install dotnet-ef --configfile %scriptPath%\deploy\NuGet-OfficialOnly.config --version "3.1.8"
@IF %ERRORLEVEL% NEQ 0 call :error "EF core installation failed"
dotnet fake run %scriptPath%\deploy\build.fsx -t Artifact 
@IF %ERRORLEVEL% NEQ 0 call :error "Build failed"
dotnet ef migrations script --project %scriptPath%\src\DbRepository\DbRepository.csproj -i -o %scriptPath%\deploy\build\artifacts\out.sql
@IF %ERRORLEVEL% NEQ 0 call :error "Migration script failed"
@goto :end
:error 
@echo %~1
@if %CMDER_CONFIGURED% EQ 1 goto :end
@exit %ERRORLEVEL%
:end
@echo "Ended"