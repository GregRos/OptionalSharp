using System;

namespace OptionalSharp {
	/// <summary>
	/// Represents an optional value, where the underlying value type is unknown. Used for abstracting over all optional types.
	/// </summary>
	public interface IAnyOptional {
		/// <summary>
		/// Indicates whether this instance wraps a value.
		/// </summary>
		bool HasValue { get; }

		/// <summary>
		/// Gets the underlying value, or throws an exception if none exists.
		/// </summary>
		object Value { get; }

		/// <summary>
		/// Returns the declared inner type of the Optional.
		/// </summary>
		/// <returns></returns>
		Type GetInnerType();
	}
}