using System;

namespace OptionalSharp {
	static class Errors {

		
		public static MissingOptionalValueException NoValue(Type t, object reason) {
			return new MissingOptionalValueException(t, reason);
		}

		public static ArgumentNullException ArgumentNull(string argName) {
			return new ArgumentNullException(argName, "The argument cannot be null.");
		}

		public static ArgumentException InvalidType(string argName) {
			return new ArgumentException("Invalid type.", argName);
		}
	}


}