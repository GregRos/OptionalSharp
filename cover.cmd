"packages\OpenCover.4.6.519\tools\OpenCover.Console.exe" ^
	-register:user ^
	-target:"packages\xunit.runner.console.2.2.0\tools\xunit.console.x86.exe" ^
	-targetargs:"OptionalSharp.Tests\bin\Debug\OptionalSharp.tests.exe -noshadow" ^
	-output:".\Coverage\Raw\coverage.xml" ^
	-filter:"+[OptionalSharp]* -[*]OptionalSharp.Errors -[*]OptionalSharp.MissingOptionalValueException -[*]OptionalSharp.ReflectExt"
