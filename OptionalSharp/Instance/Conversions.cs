#pragma warning disable 618
namespace OptionalSharp {
	public partial struct Optional<T> {
		/// <summary>
		///     Returns an optional value instance wrapping the specified value.
		/// </summary>
		/// <param name="v"></param>
		public static implicit operator Optional<T>(T v) {
			return Optional.Some(v);
		}

		/// <summary>
		///     Unwraps the optional value.
		/// </summary>
		/// <param name="v"></param>
		public static explicit operator T(Optional<T> v) {
			return v.Value;
		}

		/// <summary>
		///     Converts the <see cref="ImplicitNoneValue"/> token to a proper optional value of type <typeparamref name="T"/>, indicating a missing value.
		/// </summary>
		/// <param name="none">The none token.</param>
		public static implicit operator Optional<T>(ImplicitNoneValue none) {
			return None;
		}
	}
}