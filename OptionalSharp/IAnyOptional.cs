using System;

namespace OptionalSharp {
	/// <summary>
	/// Represents an optional value, where the underlying value type is unknown. Used for abstracting over all optional types.
	/// </summary>
	public interface IAnyOptional {
		/// <summary>
		/// Indicates whether this instance wraps a value.
		/// </summary>
		bool Exists { get; }

		/// <summary>
		/// Gets the underlying value, or throws an exception if none exists.
		/// </summary>
		object Value { get; }
	}
}