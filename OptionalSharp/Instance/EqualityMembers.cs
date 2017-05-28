namespace OptionalSharp {
	public partial struct Optional<T> {
		/// <summary>
		/// Determines if this optional value is equal to another optional value, where the underlying value type of the second optional value is unknown.
		/// </summary>
		/// <param name="other">The other optional value.</param>
		/// <returns></returns>
		public bool Equals(IAnyOptional other) {
			return (!Exists && !other.Exists) || (Exists && other.Exists && Equals(Value, other.Value));
		}

		/// <summary>
		/// Determines equality between optional values of different types. None values are always equal, and Some values are equal if the underlying values are equal.
		/// </summary>
		/// <typeparam name="T2">The type of the second optional value.</typeparam>
		/// <param name="other">The other optional value.</param>
		/// <returns></returns>
		public bool Equals<T2>(Optional<T2> other) {
			return !Exists ? !other.Exists : Exists && other.Exists && Equals(Value, other.Value);
		}

		/// <summary>
		///     Determines equality between the optional value and another object, which may be another optional value or a concrete value.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj) {
			return obj is Optional<T>
				? Equals((Optional<T>) obj) : obj is IAnyOptional ? Equals((IAnyOptional) obj) : Exists && Equals(Value, obj);
		}

		/// <summary>
		/// Determines if this optional value instance is equal to the specified concrete (non-optional) value.
		/// </summary>
		/// <param name="other">The concrete value.</param>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		public bool Equals(T other) {
			return Exists && _eq.Equals(Value, other);
		}

		/// <summary>
		///		Determines whether this optional value instance is equal to the specified optional value instance.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Optional<T> other) {
			return !other.Exists ? !Exists : Equals(other.Value);
		}

		/// <summary>
		///     Returns a hash code for this optional value instance. If it has an underlying value, the hash code of the underlying value is returned. Otherwise, a hash code of 0 is returned.
		/// </summary>
		public override int GetHashCode() {
			return !Exists ? NoneHashCode : _eq.GetHashCode(Value);
		}
	}
}