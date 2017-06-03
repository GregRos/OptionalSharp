using System;

namespace OptionalSharp {
	/// <summary>
	///     Indicates that an attempt was made to access the Value property of an Optional with no inner value.
	/// </summary>
	[Serializable]
	public class MissingOptionalValueException : InvalidOperationException {
		public object Reason { get; }

		public Type Type { get; set; }

		private static string GetMessage(Type t, object reason, string message) {
			string typeName = t == null ? "an unknown type" : "type " + t.PrettyName();
			return $"Tried to get the underlying value of an optional value of {typeName}, but no value exists. {message ?? ""}";
		}

		/// <summary>
		///     Creates a new instance of NoValueException for the option type 't'.
		/// </summary>
		/// <param name="t"></param>
		/// <param name="reason"></param>
		/// <param name="message">An optional extra message.</param>
		public MissingOptionalValueException(Type t, object reason = null, string message = "")
			: base(GetMessage(t, reason, message)) {
			Reason = reason;
			Type = t;

		}
	}
}