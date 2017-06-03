using Xunit;

namespace OptionalSharp.Tests
{
	public static partial class Tests
	{


		public static class AnyNoneToken {
			[Fact]
			static void IsValidNone() {
				var token = Optional.None();
				Assert.False(token.HasValue);
				Assert.Throws<MissingOptionalValueException>(() => token.Value);
				Assert.Equal(token.ToString(), "");
			}
		}

		public static class ConvertsToIAnyOptional {
			[Fact]
			static void None_IsNone() {
				var opt = new Optional<int>();
				IAnyOptional opt2 = opt;
				Assert.False(opt2.HasValue);
				Assert.Throws<MissingOptionalValueException>(() => opt2.Value);
			}
			[Fact]
			static void Some_IsSome() {
				var opt = Optional.Some(5);
				IAnyOptional opt2 = opt;
				Assert.True(opt2.HasValue);
				Assert.Equal(opt2.Value, 5);
			}
		}

		
	}
}
