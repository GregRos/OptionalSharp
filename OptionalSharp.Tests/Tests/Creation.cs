using Xunit;

namespace OptionalSharp.Tests {
	public static partial class Tests {
		static void IsValidSome<T>(Optional<T> some, T value)
		{
			Assert.True(some.HasValue);
			Assert.Equal(some.Value, value);
		}

		static void IsValidNone<T>(Optional<T> none)
		{
			Assert.False(none.HasValue);
			Assert.Throws<MissingOptionalValueException>(() => none.Value);
		}
		public static class Creation {

			[Fact]
			static void Ctor_Some() {
				var some = Optional.Some(5);
				IsValidSome(some, 5);
			}

			[Fact]
			static void Ctor_None() {
				var none = new Optional<int>();
				IsValidNone(none);
			}

			[Fact]
			static void Method_MakeSome() {
				var some = 5.AsOptionalSome();
				IsValidSome(some, 5);
				var some2 = ((object) null).AsOptionalSome();
				IsValidSome(some2, null);
				var some3 = ((int?) null).AsOptionalSome();
				IsValidSome(some3, null);
				var some4 = (new Optional<int>()).AsOptionalSome();
				IsValidSome(some4, Optional.NoneOf<int>());
			}

			[Fact]
			static void Method_AsOptional() {
				var some = 5.AsOptional();
				IsValidSome(some, 5);
				var none = ((object) null).AsOptional();
				IsValidNone(none);
				var none2 = ((int?) null).AsOptional();
				IsValidNone(none2);
			}

			[Fact]
			static void Method_SomeStatic() {
				var some = OptionalSharp.Optional.Some(5);
				IsValidSome(some, 5);
				var some2 = OptionalSharp.Optional.Some<object>(null);
				IsValidSome(some2, null);
				var some3 = OptionalSharp.Optional.Some<int?>(null);
				IsValidSome(some3, null);
			}

			[Fact]
			static void Method_NoneOf() {
				var none = OptionalSharp.Optional.NoneOf<int>();
				IsValidNone(none);
				var none2 = OptionalSharp.Optional.NoneOf<object>();
				IsValidNone(none2);
			}

			[Fact]
			static void Method_TokenNoneConversion() {
				var noneToken = OptionalSharp.Optional.None();
				Optional<int> none1 = noneToken;
				IsValidNone(none1);
				Optional<object> none2 = noneToken;
				IsValidNone(none2);
			}

			static Optional<int> _default;

			[Fact]
			static void UninitField_IsNone() {
				Assert.True(!_default.HasValue);
			}

			[Fact]
			static void DefaultGeneric_IsNone() {
				Assert.True(!default(Optional<int>).HasValue);
			}

		}
	}
}