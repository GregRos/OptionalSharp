rmdir /s /q nuget-package
SET MSBuild=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe
for %%x in (net35, net45, net461, net40) do (
    "%MSBuild%" "OptionalSharp\OptionalSharp.csproj" /t:Clean,Rebuild /p:Configuration=%%x
    xcopy OptionalSharp\bin\%%x\OptionalSharp.dll nuget-package\lib\%%x\ /y
    xcopy OptionalSharp\bin\%%x\OptionalSharp.xml nuget-package\lib\%%x\ /y
)

dotnet msbuild "NetStd-OptionalSharp\NetStd-OptionalSharp.csproj" /t:Rebuild /p:Configuration=Release
for %%x in (netstandard1.6, netstandard1.4) do (
    xcopy NetStd-OptionalSharp\bin\Release\%%x\OptionalSharp.dll nuget-package\lib\%%x\ /y
    xcopy NetStd-OptionalSharp\bin\Release\%%x\OptionalSharp.xml nuget-package\lib\%%x\ /y
)

xcopy OptionalSharp\OptionalSharp.nuspec nuget-package\ /y
nuget\nuget pack nuget-package\ -o nupkgs\
rmdir /s /q nuget-package