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

		/// <summary>
		///     Determines equality between an Optional and a value of the inner type of the Optional.
		/// </summary>
		/// <param name="optional">The Optional.</param>
		/// <param name="concrete">The non-Optional value.</param>
		/// <returns></returns>
		public static bool operator ==(T concrete, Optional<T> optional) {
			return optional.Equals(concrete);
		}

		/// <summary>
		///     Determines inequality between an Optional and a concrete value. The inverse of the == operator.
		/// </summary>
		/// <param name="optional">The Optional.</param>
		/// <param name="concrete">The non-Optional.</param>
		/// <returns></returns>
		public static bool operator !=(Optional<T> optional, T concrete) {
			return !(optional == concrete);
		}

		/// <summary>
		///     Determines equality between an Optional and a concrete value.
		/// </summary>
		/// <param name="optional">The Optional.</param>
		/// <param name="concrete">The concrete value.</param>
		/// <returns></returns>
		public static bool operator ==(Optional<T> optional, T concrete) {
			return optional.Equals(concrete);
		}

		/// <summary>
		///     Determines inequality between an Optional and a concrete value. The inverse of the == operator.
		/// </summary>
		/// <param name="concrete">The concrete value.</param>
		/// <param name="optional">The optional.</param>
		/// <returns></returns>
		public static bool operator !=(T concrete, Optional<T> optional) {
			return !(concrete == optional);
		}
	}
}