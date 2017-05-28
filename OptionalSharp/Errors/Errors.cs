namespace OptionalSharp {
	static class Errors {

		public static NoValueException NoValue<T>() {
			return new NoValueException(typeof (T));
		}

		public static NoValueException NoValue() {
			return new NoValueException();
		}
	}
}