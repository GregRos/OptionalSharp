using System;

namespace OptionalSharp {
	public partial struct Optional<T> {
		/// <summary>
		/// If this is Some, returns Some only if the inner value fulfills the predicate. Otherwise, returns None.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <param name="reason">Optionally, an object that describes the filter so that if it fails, the reason is recorded.</param>
		/// <returns></returns>
		public Optional<T> Filter(Func<T, bool> predicate, Optional<object> reason = default(Optional<object>)) {
			if (predicate == null) throw Errors.ArgumentNull(nameof(predicate));
			return !HasValue || !predicate(Value) ? new Optional<T>(default(T), false, reason.Or(MissingValueReason.FailedFilter)) : this;
		}

		/// <summary>
		/// Similar to <c>?.</c>. If this is Some, applies the given function on the inner value and returns the result (another Optional). Otherwise, returns None.
		/// </summary>
		/// <typeparam name="TOut">The inner type of the Optional result of the function.</typeparam>
		/// <param name="map">The function to apply, returning an Optional.</param>
		/// <returns></returns>
		public Optional<TOut> MapMaybe<TOut>(Func<T, Optional<TOut>> map) {
			if (map == null) throw Errors.ArgumentNull(nameof(map));
			return !HasValue ? Optional.NoneOf<TOut>(Reason) : map(Value);
		}

		/// <summary>
		///     Similar to <c>?.</c> If this is Some, applies the given function on the inner value and wraps the result in Some. Otherwise, returns None.
		/// </summary>
		/// <typeparam name="TOut">The inner type of the Optional result.</typeparam>
		/// <param name="map">The function to apply.</param>
		/// <returns></returns>
		public Optional<TOut> Map<TOut>(Func<T, TOut> map) {
			if (map == null) throw Errors.ArgumentNull(nameof(map));
			return !HasValue ? Optional.NoneOf<TOut>(Reason) : Optional.Some(map(this.Value));
		}

		/// <summary>
		///     Similar to <c>as</c>. Tries to cast the inner value to a different type, returning None if there is no inner value or the conversion fails.
		/// </summary>
		/// <typeparam name="TOut">The type to convert to.</typeparam>
		/// <returns></returns>
		public Optional<TOut> As<TOut>() {
			if (!HasValue) return Optional.NoneOf<TOut>(Reason);
			if (Value is TOut) {
				return Optional.Some((TOut) (object) Value);
			}
			return Optional.NoneOf<TOut>(MissingValueReason.FailedCast<T, TOut>.Reason);
		}

		/// <summary>
		///		Similar to a cast. Casts the inner value to a different type, returning None if there is no inner value.
		/// </summary>
		/// <typeparam name="TOut">The type to cast to.</typeparam>
		/// <exception cref="InvalidCastException">Thrown if the conversion fails.</exception>
		public Optional<TOut> Cast<TOut>() {
			return !HasValue ? Optional.None() : Optional.Some((TOut) (object) Value);
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
		///		Returns the inner value or, if none exists, calls the given factory method to create a value.
		/// </summary>
		/// <param name="factory">The factory method to generate a value.</param>
		/// <returns></returns>
		public T OrCall(Func<T> factory) {
			return HasValue ? Value : factory();
		}

		/// <summary>
		///		Similar to <c>??</c>. Returns this instance's inner value, or the other default value if this is None.
		/// </summary>
		/// <param name="default">The default value.</param>
		/// <returns></returns>
		public T Or(T @default) {
			return this.HasValue ? this.Value : @default;
		}

		/// <summary>
		/// Returns an Optional with its Reason property set to the given value, describing why no value is available.
		/// </summary>
		/// <param name="reason">A reason object describing why no value is available.</param>
		/// <returns></returns>
		public Optional<T> WithReason(object reason) {
			return HasValue ? this : new Optional<T>(_value, false, reason);
		}

	}
}