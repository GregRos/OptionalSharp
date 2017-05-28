using System;

namespace OptionalSharp {
	static class Errors {

		public static MissingOptionalValueException NoValue<T>() {
			return new MissingOptionalValueException(typeof (T));
		}

		public static MissingOptionalValueException NoValue() {
			return new MissingOptionalValueException();
		}

		public static ArgumentNullException ArgumentNull(string argName) {
			return new ArgumentNullException(argName, "The argument cannot be null.");
		}

		public static ArgumentException InvalidType(string argName) {
			return new ArgumentException("Invalid type.", argName);
		}
	}
}