namespace OptionalSharp {
	public partial struct Optional<T> {
		/// <summary>
		///     Determines equality between Optionals, where the inner type of the right-hand Optional is not known.
		/// </summary>
		/// <param name="a">The left-hand instance, the inner type of which is known.</param>
		/// <param name="b">The right-hand instance</param>
		/// <returns></returns>
		public static bool operator ==(Optional<T> a, IAnyOptional b) {
			return a.Equals(b);
		}

		/// <summary>
		///     Determines inequality between Optionals,
		///		where the underlying value type of the right-hand instance is unknown. The inverse of the == operator.
		/// </summary>
		/// <param name="a">The left-hand instance, the inner type of which is known.</param>
		/// <param name="b">The right-hand instance, the type of which is unknown.</param>
		/// <returns></returns>
		public static bool operator !=(Optional<T> a, IAnyOptional b) {
			return !(a == b);
		}

		/// <summary>
		///     Determines equality between Optionals with the same inner type.
		/// </summary>
		/// <returns></returns>
		public static bool operator ==(Optional<T> a, Optional<T> b) {
			return a.Equals(b);
		}

		/// <summary>
		///     Determines inequality between Optionals with the same inner type.
		/// </summary>
		/// <param name="a">The first Optional.</param>
		/// <param name="b">The second Optional.</param>
		/// <returns></returns>
		public static bool operator !=(Optional<T> a, Optional<T> b) {
			return !(a == b);
		}

	}
}