using System;
using Xunit;

namespace OptionalSharp.Tests {

	public static partial class Tests {
		public static class Transforms {

			public static class Or {
				[Fact]
				static void NoneElseVal()
				{
					Assert.Equal(Optional.NoneOf<int>("test").Or(5), 5);
				}
				[Fact]
				static void NoneElseNone() {
					var orMaybe = Optional.NoneOf<object>("a").OrMaybe(Optional.NoneOf<object>("b"));
					Assert.Equal(orMaybe, Optional.None());
					Assert.Equal(orMaybe.Reason, "b");
				}
				[Fact]
				static void NoneElseSome() {
					Assert.Equal(Optional.NoneOf<int>("a").OrMaybe(Optional.Some(5)), 5.AsOptionalSome());
				}
				[Fact]
				static void NoneElseNull()
				{
					Assert.Equal(Optional.NoneOf<object>("a").Or(null), null);
				}
				[Fact]
				static void SomeElseVal()
				{
					Assert.Equal(Optional.Some(5).Or(6), 5);
				}
				[Fact]
				static void SomeElseSome()
				{
					Assert.Equal(Optional.Some(5).OrMaybe(Optional.Some(4)), Optional.Some(5));
				}
				[Fact]
				static void SomeElseNone() {
					var orMaybe = Optional.Some(5).OrMaybe(Optional.None("a"));
					Assert.Equal(orMaybe, Optional.Some(5));
				}

				[Fact]
				static void OrCall() {
					Assert.Equal(Optional.Some(5).OrCall(() => 6), 5);
					Assert.Equal(Optional.NoneOf<int>().OrCall(() => 6), 6);
				}
			}

			public static class Map {
				[Fact]
				static void NoneMap() {
					var expected = Optional.NoneOf<int>("a").Select(x => 5);
					Assert.Equal(expected, Optional.None());
					Assert.Equal(expected.Reason, "a");
				}

				[Fact]
				static void SomeMapNull() {
					Assert.Equal(Optional.Some(5).Select(x => (object)null), null);
				}
				[Fact]
				static void SomeMapVal() {
					Assert.Equal(Optional.Some(5).Select(x => x + 5), 10);
				}
				[Fact]
				static void SomeMapSome() {
					Assert.Equal(Optional.Some(5).SelectMaybe(x => Optional.Some(x + 5)), 10);
				}
				[Fact]
				static void SomeMapNone() {
					Assert.Equal(Optional.Some(5).SelectMaybe(x => Optional.NoneOf<int>()), Optional.None());
				}

				[Fact]
				static void CastSome() {
					Assert.Equal(Optional.Some(5).Cast<object>(), 5);
				}

				[Fact]
				static void CastNone() {
					var expected = Optional.NoneOf<int>("a").Cast<object>();
					Assert.Equal(expected, Optional.None());
					Assert.Equal(expected.Reason, "a");
				}

				[Fact]
				static void WithReason() {
					var expected = Optional.NoneOf<int>("a").WithReason("b");
					Assert.Equal(expected, Optional.None());
					Assert.Equal(expected.Reason, "b");
				}

				[Fact]
				static void BadCastNone() {
					var expected = Optional.NoneOf<int>("a").Cast<string>();
					Assert.Equal(expected, Optional.None());
					Assert.Equal(expected.Reason, "a");
				}

				[Fact]
				static void BadCastSome() {
					Assert.Throws<InvalidCastException>(() => Optional.Some(5).Cast<string>());
				}

				[Fact]
				static void AsSomeSuccess() {
					Assert.Equal(Optional.Some(5).OfType<object>(), 5);
				}

				[Fact]
				static void AsSomeFail() {
					var ofType = Optional.Some(5).OfType<string>();
					Assert.Equal(ofType, Optional.None());
					Assert.Equal(ofType.Reason, MissingValueReason.FailedCast<int, string>.Reason);
				}

				[Fact]
				static void AsNone() {
					var ofType = Optional.NoneOf<int>("a").OfType<object>();
					Assert.Equal(ofType, Optional.None());
					Assert.Equal(ofType.Reason, "a");
				}

				[Fact]
				static void FilterSomeSuccess() {
					Assert.Equal(Optional.Some(5).Where(x => x == 5, "test"), 5);
				}

				[Fact]
				static void FilterSomeFailure() {
					var expected = Optional.Some(5).Where(x => x != 5, "a");
					Assert.Equal(expected, Optional.None());
					Assert.Equal(expected.Reason, "a");
				}

				[Fact]
				static void FilterNone() {
					var expected = Optional.NoneOf<int>("a").Where(x => x != 5, "b");
					Assert.Equal(expected, Optional.None());
					Assert.Equal(expected.Reason, "a");
				}
			}

			public static class OrError {
				[Fact]
				static void ValueOrError() {
					var x = Optional.NoneOf<int>();
					Assert.Throws<DllNotFoundException>(() => x.ValueOrError(new DllNotFoundException("test")));
				}
			}

			public static class Flatten {
				[Fact]
				static void FlattenObj() {
					var x = Optional.Some<object>(null);
					Assert.Equal(x.Flatten(), Optional.None());
				}
				[Fact]
				static void FlattenNested() {
					var x = Optional.Some(Optional.Some(5));
					Assert.Equal(x, Optional.Some(5));
					Assert.Equal(x.Value.Value, (object)5);
					Optional<int> flat = x.Flatten();
					Assert.Equal(flat.Value, 5);
				}
				[Fact]
				static void FlattenOptionalOfNullable() {
					var someNull = Optional.Some((int?) null);
					Optional<int> flat = someNull.Flatten();
					Assert.Equal(flat, Optional.None());
					Assert.Equal(flat.Reason, MissingValueReason.ConvertedFromNull);
					var someVal = Optional.Some((int?) 5);
					flat = someVal.Flatten();
					Assert.Equal(flat, 5);
				}
				[Fact]
				static void FlattenNullableOfOptional() {
					var someNull = (Optional<int>?) null;
					Optional<int> flat = someNull.Flatten();
					Assert.Equal(flat, Optional.None());
					Assert.Equal(flat.Reason, MissingValueReason.ConvertedFromNull);
					var someVal = (Optional<int>?) Optional.Some(5);
					flat = someVal.Flatten();
					Assert.Equal(flat, 5);
				}
			}

			

		

		}
	}
}