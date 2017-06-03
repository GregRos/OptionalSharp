using Xunit;

namespace OptionalSharp.Tests {
	public static partial class Tests {
		public static class Stringify {
			[Fact]
			static void Optional_Some_ToDebugString()
			{
				var opt = Optional.Some(5);
				Assert.Equal(opt.ToDebugString(), "Some<Int32>(5)");
			}

			[Fact]
			static void Optional_None_ToDebugString() {
				var opt = Optional.NoneOf<int>();
				Assert.Equal(opt.ToDebugString(), "NoneOf<Int32>");
			}
			[Fact]
			static void Optional_Some_ToString() {
				var opt = Optional.Some(5);
				Assert.Equal(opt.ToString(), "5");
			}
			[Fact]
			static void Optional_None_ToString() {
				var opt = Optional.NoneOf<int>();
				Assert.Equal(opt.ToString(), "");
			}
			[Fact]
			static void NoneToken_ToString() {
				var none = Optional.None();
				Assert.Equal(none.ToString(), "");
			}

		}
	}
}