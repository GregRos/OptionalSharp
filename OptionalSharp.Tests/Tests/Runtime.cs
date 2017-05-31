using Xunit;

namespace OptionalSharp.Tests {
	public static partial class Tests {
		public static class Runtime {
			[Fact]
			static void Optional_GetInnerType() {
				var opt = Optional.Some(5);
				Assert.Equal(opt.GetInnerType(), typeof(int));
			}
			[Fact]
			static void NoneToken_GetInnerType() {
				var none = Optional.None;
				Assert.Equal(none.GetInnerType(), typeof(object));
			}

			[Fact]
			static void RuntimeCreateOptional_OverridenType() {
				var x = Optional.RuntimeCreateSome("hello", typeof(object));
				IsValidSome((Optional<object>)x, "hello");
			}
			[Fact]
			static void RuntimeCreateOptional_DefaultType() {
				var x = Optional.RuntimeCreateSome("hello");
				IsValidSome((Optional<string>)x, "hello");
			}
			[Fact]
			static void RuntimeCreateNone() {
				var x = Optional.RuntimeCreateNone(typeof(string));
				IsValidNone((Optional<string>)x);
			}
		}
	}
}