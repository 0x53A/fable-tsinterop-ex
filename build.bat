@echo off

dotnet restore build.csproj

if "%*." == "." (
	echo dotnet fake build "build.fsx"
	dotnet fake build
)else (
	REM we have arguments ...
	echo dotnet fake -v build "build.fsx" --target %*
	dotnet fake build --target %*
)
