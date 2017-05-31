using System;
using Xunit;

namespace OptionalSharp.Tests {

	public static partial class Tests {
		public static class Transforms {

			public static class Or {
				[Fact]
				static void NoneElseVal()
				{
					Assert.Equal(Optional.NoneOf<int>().Or(5), 5);
				}
				[Fact]
				static void NoneElseNone()
				{
					Assert.Equal(Optional.NoneOf<object>().OrMaybe(Optional.NoneOf<object>()), Optional.None);
				}
				[Fact]
				static void NoneElseSome()
				{
					Assert.Equal(Optional.NoneOf<int>().OrMaybe(Optional.Some(5)), 5);
				}
				[Fact]
				static void NoneElseNull()
				{
					Assert.Equal(Optional.NoneOf<object>().Or(null), null);
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
				static void SomeElseNone()
				{
					Assert.Equal(Optional.Some(5).OrMaybe(Optional.None), Optional.Some(5));
				}
			}

			public static class Map {
				[Fact]
				static void NoneMap() {
					Assert.Equal(Optional.NoneOf<int>().Map(x => 5), Optional.None);
				}
				[Fact]
				static void NoneMapNone() {
					Assert.Equal(Optional.NoneOf<string>().Map(x => 8), Optional.None);
				}

				[Fact]
				static void SomeMapNull() {
					Assert.Equal(Optional.Some(5).Map(x => (object)null), null);
				}
				[Fact]
				static void SomeMapVal() {
					Assert.Equal(Optional.Some(5).Map(x => x + 5), 10);
				}
				[Fact]
				static void SomeMapSome() {
					Assert.Equal(Optional.Some(5).MapMaybe(x => Optional.Some(x + 5)), 10);
				}
				[Fact]
				static void SomeMapNone() {
					Assert.Equal(Optional.Some(5).MapMaybe(x => Optional.NoneOf<int>()), Optional.None);
				}

				[Fact]
				static void CastSome() {
					Assert.Equal(Optional.Some(5).Cast<object>(), 5);
				}

				[Fact]
				static void CastNone() {
					Assert.Equal(Optional.NoneOf<int>().Cast<object>(), Optional.None);
				}

				[Fact]
				static void BadCastNone() {
					Assert.Equal(Optional.NoneOf<int>().Cast<string>(), Optional.None);
				}

				[Fact]
				static void BadCastSome() {
					Assert.Throws<InvalidCastException>(() => Optional.Some(5).Cast<string>());
				}

				[Fact]
				static void AsSomeSuccess() {
					Assert.Equal(Optional.Some(5).As<object>(), 5);
				}

				[Fact]
				static void AsSomeFail() {
					Assert.Equal(Optional.Some(5).As<string>(), Optional.None);
				}

				[Fact]
				static void AsNone() {
					Assert.Equal(Optional.NoneOf<int>().As<object>(), Optional.None);
				}

				[Fact]
				static void FilterSomeSuccess() {
					Assert.Equal(Optional.Some(5).Filter(x => x == 5), 5);
				}

				[Fact]
				static void FilterSomeFailure() {
					Assert.Equal(Optional.Some(5).Filter(x => x != 5), Optional.None);
				}

				[Fact]
				static void FilterNone() {
					Assert.Equal(Optional.NoneOf<int>().Filter(x => x != 5), Optional.None);
				}
			}

			public static class Flatten {
				[Fact]
				static void FlattenObj() {
					var x = Optional.Some<object>(null);
					Assert.Equal(x.Flatten(), Optional.None);
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
					Assert.Equal(flat, Optional.None);
					var someVal = Optional.Some((int?) 5);
					flat = someVal.Flatten();
					Assert.Equal(flat, 5);
				}
				[Fact]
				static void FlattenNullableOfOptional() {
					var someNull = (Optional<int>?) null;
					Optional<int> flat = someNull.Flatten();
					Assert.Equal(flat, Optional.None);
					var someVal = (Optional<int>?) Optional.Some(5);
					flat = someVal.Flatten();
					Assert.Equal(flat, 5);
				}
			}

			

		

		}
	}
}