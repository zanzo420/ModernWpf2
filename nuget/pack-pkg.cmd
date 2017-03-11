@echo off

@echo compiling solution...
if exist "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\VsDevCmd.bat" (
	set vcbat="C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\VsDevCmd.bat"
)

IF NOT DEFINED vcbat (
	set error="No Compatible Visual Studio (2017) found."
	goto error;
)

call %vcbat% x86
msbuild ..\ModernWpf2.sln /t:Build /p:Configuration=Release /m

@echo packing nuget
nuget pack ..\src\ModernWpf.Core\ModernWpf.Core.csproj -Prop Configuration=Release
nuget pack ..\src\ModernWpf\ModernWpf.csproj -IncludeReferencedProjects -Prop Configuration=Release
goto end;

:error
echo Error: %error%

:end
pause