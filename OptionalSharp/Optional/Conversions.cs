#pragma warning disable 618
namespace OptionalSharp {
	public partial struct Optional<T> {
		/// <summary>
		///     Implicitly converts a concrete value of type <typeparamref name="T"/> into an Optional in its Some state, wrapping that value.
		/// </summary>
		/// <param name="innerValue">The inner value of the resulting optional.</param>
		public static implicit operator Optional<T>(T innerValue) {
			return Optional.Some(innerValue);
		}

		/// <summary>
		///     Explicitly converts an <see cref="Optional{T}"/> by returning its inner value, if one exists. If no inner value exists, an exception is thrown.
		/// </summary>
		/// <param name="optional">The optional to convert. Must be in its Some state or an exception is thrown.</param>
		/// <exception cref="MissingOptionalValueException">Thrown by the provided Optional is in its None state, i.e. it has no inner value.</exception>
		public static explicit operator T(Optional<T> optional) {
		 	return optional.Value;
		}

		/// <summary>
		///     Converts an <see cref="ImplicitNoneValue"/> to a proper <see cref="Optional{T}"/> in its None state, i.e. without an inner value.
		/// </summary>
		/// <param name="none">The none token.</param>
		public static implicit operator Optional<T>(ImplicitNoneValue none) {
			return None;
		}
	}
}