using System;

namespace OptionalSharp {
	public partial struct Optional<T> {
		/// <summary>
		/// Applies a filter on the underlying value, returning None if the filter returns false.
		/// </summary>
		/// <param name="filter">The predicate.</param>
		/// <returns></returns>
		public Optional<T> Filter(Func<T, bool> filter) {
			if (filter == null) throw Errors.ArgumentNull(nameof(filter));
			return !HasValue || !filter(Value) ? None : this;
		}

		/// <summary>
		/// Applies the specified function on the underlying value, if one exists, and returns the result. Otherwise, returns None.
		/// </summary>
		/// <typeparam name="T">The type of the input optional value.</typeparam>
		/// <typeparam name="TOut">The type of the output optional value.</typeparam>
		/// <param name="map">The function to apply, returning an optional value of a potentially different type.</param>
		/// <returns></returns>
		public Optional<TOut> MapMaybe<TOut>(Func<T, Optional<TOut>> map) {
			if (map == null) throw Errors.ArgumentNull(nameof(map));
			return !HasValue ? Optional<TOut>.None : map(Value);
		}

		/// <summary>
		///     Applies the specified function on the underlying value, if one exists, and wraps the result in an optional value. Otherwise, returns None. Similar to the conditional access ?. operator.
		/// </summary>
		/// <typeparam name="T">The type of the input optional value.</typeparam>
		/// <typeparam name="TOut">The type of the output optional value.</typeparam>
		/// <param name="map">The function to apply.</param>
		/// <returns></returns>
		public Optional<TOut> Map<TOut>(Func<T, TOut> map) {
			if (map == null) throw Errors.ArgumentNull(nameof(map));
			return !HasValue ? Optional<TOut>.None : Optional.Some(map(this.Value));
		}

		/// <summary>
		///     Casts the underlying value to a different type (if one exists), returning a missing value if the conversion fails.
		/// </summary>
		/// <typeparam name="TOut">The type of the output value.</typeparam>
		/// <returns></returns>
		public Optional<TOut> As<TOut>() {
			return HasValue && Value is TOut ? Optional.Some((TOut) (object) Value) : Optional.None;
		}

		/// <summary>
		///		Casts the underlying value to a different type (if one exists), throwing InvalidCastException if the operation fails.
		/// </summary>
		/// <typeparam name="TOut">The type to cast to.</typeparam>
		/// <exception cref="InvalidCastException">Thrown if the conversion fails.</exception>
		public Optional<TOut> Cast<TOut>() {
			return !HasValue ? Optional.None : Optional.Some((TOut) (object) Value);
		}

		/// <summary>
		/// Returns this instance if it has an underlying value, and otherwise returns the other optional value instance (whether it has a value or not).
		/// </summary>
		/// <param name="other">The other optional value instance.</param>
		/// <returns></returns>
		public Optional<T> OrMaybe(Optional<T> other) {
			return HasValue ? this : other;
		}

		/// <summary>
		/// Returns the underlying value, or the specified default value if no underlying value exists.
		/// </summary>
		/// <param name="default">The default value, returned if this instance has no value.</param>
		/// <returns></returns>
		public T Or(T @default) {
			return this.HasValue ? this.Value : @default;
		}

	}
}