namespace OptionalSharp {
	public partial struct Optional<T> {
		/// <summary>
		///     Determines equality between optional values, where the underlying value type of the right-hand instance is unknown.
		/// </summary>
		/// <param name="a">The left-hand instance, the type of which is known.</param>
		/// <param name="b">The right-hand instance, the type of which is unknown.</param>
		/// <returns></returns>
		public static bool operator ==(Optional<T> a, IAnyOptional b) {
			return a.Equals(b);
		}

		/// <summary>
		///     Determines inequality between optional value instances,
		///		 where the underlying value type of the right-hand instance is unknown. The inverse of the == operator.
		/// </summary>
		/// <param name="a">The left-hand instance, the type of which is known.</param>
		/// <param name="b">The right-hand instance, the type of which is unknown.</param>
		/// <returns></returns>
		public static bool operator !=(Optional<T> a, IAnyOptional b) {
			return !(a == b);
		}

		/// <summary>
		///     Determines equality between optional value instances, where the underlying value is of the same type.
		/// </summary>
		/// <returns></returns>
		public static bool operator ==(Optional<T> a, Optional<T> b) {
			return a.Equals(b);
		}

		/// <summary>
		///     Determines inequality between optional value instances. The inverse of the == operator.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator !=(Optional<T> a, Optional<T> b) {
			return !(a == b);
		}

		/// <summary>
		///     Determines equality between an optional value and a concrete value.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static bool operator ==(T other, Optional<T> self) {
			return self.Equals(other);
		}

		/// <summary>
		///     Determines inequality between an optional value and a concrete value. The inverse of the == operator.
		/// </summary>
		/// <returns></returns>
		public static bool operator !=(Optional<T> a, T other) {
			return !(a == other);
		}

		/// <summary>
		///     Determines equality between an optional value and a concrete value.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator ==(Optional<T> a, T b) {
			return a.Equals(b);
		}

		/// <summary>
		///     Determines inequality between an optional value and a concrete value. The inverse of the == operator.
		/// </summary>
		/// <returns></returns>
		public static bool operator !=(T other, Optional<T> self) {
			return !(other == self);
		}
	}
}