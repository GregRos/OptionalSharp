using System;
using Xunit;

namespace OptionalSharp.Tests {
	public static partial class Tests {
		public static class Equality {
			class CustomType : IEquatable<CustomType> {
				public override bool Equals(object obj) {
					if (ReferenceEquals(null, obj)) return false;
					if (ReferenceEquals(this, obj)) return true;
					if (obj.GetType() != this.GetType()) return false;
					return Equals((CustomType) obj);
				}

				readonly int _a;

				public CustomType(int a) {
					this._a = a;
				}

				public bool Equals(CustomType other) {
					if (ReferenceEquals(null, other)) return false;
					if (ReferenceEquals(this, other)) return true;
					return _a == other._a;
				}

				public override int GetHashCode() {
					return _a;
				}

				public static bool operator ==(CustomType left, CustomType right) {
					return Equals(left, right);
				}

				public static bool operator !=(CustomType left, CustomType right) {
					return !Equals(left, right);
				}

			}

			static void OptionalsEqual<T1>(Optional<T1> a, object b) {
				Assert.True(a.Equals((object) b));

				if (b is IAnyOptional i) {
					Assert.True(a.Equals(i));
					Assert.True(a == i);
					Assert.True(!(a != i));
				}
				if (b != null) {
					Assert.Equal(a.GetHashCode(), b.GetHashCode());
				}
				
				if (b is Optional<T1> o) {
					Assert.True(a.Equals(o));
					Assert.True(a == o);
					Assert.True(!(a != o));
				}
			}

			static void OptionalsNotEqual<T1>(Optional<T1> a, object b) {
				Assert.False(a.Equals((object) b));

				if (b is IAnyOptional i) {
					Assert.False(a.Equals(i));
					Assert.False(a == i);
					Assert.False(!(a != i));
				}

				if (b is Optional<T1> o) {
					Assert.False(a.Equals(o));
					Assert.False(a == o);
					Assert.False(!(a != o));
				}
			}


			public static class AllNonesAreEqual {
				[Fact]
				static void NonesOfSameType() {
					OptionalsEqual(Optional.NoneOf<int>(), Optional.NoneOf<int>());
				}

				[Fact]
				static void NonesOfRelatedTypes() {
					var none1 = Optional.NoneOf<int>();
					var none2 = Optional.NoneOf<object>();
					OptionalsEqual(none1, none2);
					Assert.True(none2.Equals(none1));
				}

				[Fact]
				static void NoneAndToken() {
					var none1 = Optional.NoneOf<int>();
					var none2 = Optional.None;
					OptionalsEqual(none1, none2);
					Assert.True(none2.Equals(none1));
				}

				[Fact]
				static void NoneOfCustomType() {
					var none1 = Optional.NoneOf<CustomType>();
					var none2 = Optional.NoneOf<int>();
					OptionalsEqual(none1, none2);
				}
			}

			public static class SomeAreEqual {
				[Fact]
				static void SomeOfSameValue() {
					var some1 = Optional.Some(5);
					var some2 = Optional.Some(5);
					OptionalsEqual(some1, some2);
				}

				[Fact]
				static void SomeOfSameValueDifferentTypes() {
					var some1 = Optional.Some(5);
					var some2 = Optional.Some((object) 5);
					OptionalsEqual(some1, some2);
				}

				[Fact]
				static void SomeOfNullAndNullable() {
					var some1 = Optional.Some((object) null);
					var some2 = Optional.Some((int?) null);
					OptionalsEqual(some1, some2);
				}

				[Fact]
				static void SomeOfCustomType() {
					var some1 = Optional.Some(new CustomType(5));
					var some2 = Optional.Some(new CustomType(5));
					OptionalsEqual(some1, some2);
				}
			}

			public static class SomeAndValueAreEqual {
				[Fact]
				static void SameValue() {
					var some1 = Optional.Some(5);
					var value = 5;
					OptionalsEqual(some1, value);
					var some2 = Optional.Some<object>(null);
					OptionalsEqual(some2, null);
				}

				[Fact]
				static void CustomType() {
					var some1 = new CustomType(5).AsOptionalSome();
					var value = new CustomType(5);
					OptionalsEqual(some1, value);
				}
			}

			public static class Different {
				[Fact]
				static void NoneAndValue() {
					var none = Optional.NoneOf<int>();
					var value = 5;
					OptionalsNotEqual(none, value);
				}

				[Fact]
				static void NoneAndSomeOfNull()
				{
					var some = Optional.Some<object>(null);
					var none = Optional.NoneOf<object>();
					OptionalsNotEqual(some, none);
				}

				[Fact]
				static void SameType()
				{
					var some = Optional.Some(5);
					var none = Optional.NoneOf<int>();
					OptionalsNotEqual(some, none);
				}
				[Fact]
				static void BothNoneAndNonNullSomeFromNull() {
					var some = Optional.Some(new object());
					OptionalsNotEqual(some, null);
					OptionalsNotEqual(Optional.NoneOf<object>(), null);
				}
			}

		}

	}
}