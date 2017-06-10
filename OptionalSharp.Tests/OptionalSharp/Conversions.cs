using Xunit;

namespace OptionalSharp.Tests {
	using static Optional;
	public static partial class Tests {
		public static class Conversions {
			[Fact]
			static void ToClass() {
				Assert.Equal(Some("a").ToClass(), "a");
				Assert.Equal(NoneOf<string>().ToClass(), null);

			}
			[Fact]
			static void ToNullable() {
				Assert.Equal(Some(5).ToNullable(), 5);
				Assert.Equal(NoneOf<int>().ToNullable(), null);
			}
			[Fact]
			static void ToEnumerable() {
				Assert.Equal(Some(5).ToEnumerable(), new[] {5});
				Assert.Equal(NoneOf<int>().ToEnumerable(), new int[] {});
			}
			[Fact]
			static void ExplicitConversionToValue() {
				Assert.Equal((int)Some(5), 5);
			}


			
		}
	}
}