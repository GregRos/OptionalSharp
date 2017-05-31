using System;

namespace OptionalSharp {
	/// <summary>
	///     Indicates that an attempt was made to access the Value property of an Optional with no inner value.
	/// </summary>
	public class MissingOptionalValueException : InvalidOperationException {

		private static string GetMessage(Type t, string message) {
			string typeName = t == null ? "an unknown type" : "type " + t.PrettyName();
			return $"Tried to get the underlying value of an optional value of {typeName}, but no value exists. {message ?? ""}";
		}

		/// <summary>
		///     Creates a new instance of NoValueException for the option type 't'.
		/// </summary>
		/// <param name="t"></param>
		/// <param name="message">An optional extra message.</param>
		public MissingOptionalValueException(Type t = null, string message = "")
			: base(GetMessage(t, message)) {
			
		}
	}
}