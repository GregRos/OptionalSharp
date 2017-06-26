using System;

namespace OptionalSharp.More
{
	internal static class Errors
	{
		public static ArgumentException EnumExpected(Type t) {
			return new ArgumentException($"The type {t.PrettyName()} was expected to be an enum.");
		}
		public static ArgumentException InvalidParseType(Type t) {
			return new ArgumentException($"Tried to parse a string into type {t.PrettyName()}, but it is not one of the supported types.");
		}

		public static ArgumentException ExpectedNoMoreThanOneElement() {
			return new ArgumentException("Expected no more than one element");
		}

		public static ArgumentNullException ArgumentNull(string argName) {
			return new ArgumentNullException(argName, "The argument cannot be null.");
		}

		public static ArgumentOutOfRangeException ArgumentCannotBeNegative(string argName, int index) {
			return new ArgumentOutOfRangeException(argName, index, "The argument cannot be negative.");
		}
	}
}
