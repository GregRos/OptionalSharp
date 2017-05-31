using System;

namespace OptionalSharp {
	public partial struct Optional<T> {
		/// <summary>
		/// If this is Some, returns Some only if the inner value fulfills the predicate. Otherwise, returns None.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		public Optional<T> Filter(Func<T, bool> predicate) {
			if (predicate == null) throw Errors.ArgumentNull(nameof(predicate));
			return !HasValue || !predicate(Value) ? None : this;
		}

		/// <summary>
		/// Similar to <c>?.</c>. If this is Some, applies the given function on the inner value and returns the result (another Optional). Otherwise, returns None.
		/// </summary>
		/// <typeparam name="TOut">The inner type of the Optional result of the function.</typeparam>
		/// <param name="map">The function to apply, returning an Optional.</param>
		/// <returns></returns>
		public Optional<TOut> MapMaybe<TOut>(Func<T, Optional<TOut>> map) {
			if (map == null) throw Errors.ArgumentNull(nameof(map));
			return !HasValue ? Optional<TOut>.None : map(Value);
		}

		/// <summary>
		///     Similar to <c>?.</c> If this is Some, applies the given function on the inner value and wraps the result in Some. Otherwise, returns None.
		/// </summary>
		/// <typeparam name="TOut">The inner type of the Optional result.</typeparam>
		/// <param name="map">The function to apply.</param>
		/// <returns></returns>
		public Optional<TOut> Map<TOut>(Func<T, TOut> map) {
			if (map == null) throw Errors.ArgumentNull(nameof(map));
			return !HasValue ? Optional<TOut>.None : Optional.Some(map(this.Value));
		}

		/// <summary>
		///     Similar to <c>as</c>. Tries to cast the inner value to a different type, returning None if there is no inner value or the conversion fails.
		/// </summary>
		/// <typeparam name="TOut">The type to convert to.</typeparam>
		/// <returns></returns>
		public Optional<TOut> As<TOut>() {
			return HasValue && Value is TOut ? Optional.Some((TOut) (object) Value) : Optional.None;
		}

		/// <summary>
		///		Similar to a cast. Casts the inner value to a different type, returning None if there is no inner value.
		/// </summary>
		/// <typeparam name="TOut">The type to cast to.</typeparam>
		/// <exception cref="InvalidCastException">Thrown if the conversion fails.</exception>
		public Optional<TOut> Cast<TOut>() {
			return !HasValue ? Optional.None : Optional.Some((TOut) (object) Value);
		}

		/// <summary>
		///		Similar to <c>??</c>. Returns the other Optional if this Optional is a None.
		/// </summary>
		/// <param name="other">The other optional value instance.</param>
		/// <returns></returns>
		public Optional<T> OrMaybe(Optional<T> other) {
			return HasValue ? this : other;
		}

		/// <summary>
		///		Similar to <c>??</c>. Returns this instance's inner value, or the other default value if this is None.
		/// </summary>
		/// <param name="default">The default value.</param>
		/// <returns></returns>
		public T Or(T @default) {
			return this.HasValue ? this.Value : @default;
		}

	}
}