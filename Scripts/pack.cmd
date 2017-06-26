cd ..
rmdir /s /q nuget-package
SET MSBuild=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe
for %%x in (net35, net45, net461, net40) do (
    "%MSBuild%" "OptionalSharp\OptionalSharp.csproj" /t:Clean,Rebuild /p:Configuration=%%x
    xcopy OptionalSharp\bin\%%x\OptionalSharp.dll nuget-package\optional\lib\%%x\ /y
    xcopy OptionalSharp\bin\%%x\OptionalSharp.xml nuget-package\optional\lib\%%x\ /y

	"%MSBuild%" "OptionalSharp.More\OptionalSharp.More.csproj" /t:Clean,Rebuild /p:Configuration=%%x
	xcopy OptionalSharp.More\bin\%%x\OptionalSharp.More.dll nuget-package\optional.more\lib\%%x\ /y
    xcopy OptionalSharp.More\bin\%%x\OptionalSharp.More.xml nuget-package\optional.more\lib\%%x\ /y
)

dotnet msbuild "NetStd-OptionalSharp\NetStd-OptionalSharp.csproj" /t:Rebuild /p:Configuration=Release
dotnet msbuild "NetStd-OptionalSharp.More\NetStd-OptionalSharp.More.csproj" /t:Rebuild /p:Configuration=Release
for %%x in (netstandard1.6, netstandard1.4) do (
    xcopy NetStd-OptionalSharp\bin\Release\%%x\OptionalSharp.dll nuget-package\optional\lib\%%x\ /y
    xcopy NetStd-OptionalSharp\bin\Release\%%x\OptionalSharp.xml nuget-package\optional\lib\%%x\ /y
	xcopy NetStd-OptionalSharp.More\bin\Release\%%x\OptionalSharp.More.dll nuget-package\optional.more\lib\%%x\ /y
    xcopy NetStd-OptionalSharp.More\bin\Release\%%x\OptionalSharp.More.xml nuget-package\optional.more\lib\%%x\ /y
)

xcopy OptionalSharp\OptionalSharp.nuspec nuget-package\optional\ /y
xcopy OptionalSharp.More\OptionalSharp.More.nuspec nuget-package\optional.more\ /y

nuget\nuget pack nuget-package\optional\ -o nupkgs\
nuget\nuget pack nuget-package\optional.more\ -o nupkgs\
rmdir /s /q nuget-package