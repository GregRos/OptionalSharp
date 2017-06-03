using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace OptionalSharp {
	/// <summary>
	///     Static class with utility and extension methods for optional values.
	/// </summary>
	public static class OptionalExtensions {

		/// <summary>
		/// Returns a new <see cref="Optional{T}"/> in its Some state, wrapping the specified value. Use this method to wrap a <c>null</c> as Some.
		/// </summary>
		/// <typeparam name="T">The type of the inner value.</typeparam>
		/// <param name="this">The inner value.</param>
		/// <returns></returns>
		public static Optional<T> AsOptionalSome<T>(this T @this) {
			return new Optional<T>(@this);
		}

		/// <summary>
		/// Converts the nullable value to an <see cref="Optional{T}"/>, mapping a <c>null</c> to a None.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="x">The value.</param>
		/// <param name="whyNull">A reason for why the original value might be null.</param>
		/// <returns></returns>
		public static Optional<T> AsOptional<T>(this T? x, Optional<object> whyNull = default(Optional<object>))
			where T : struct {
			return x.HasValue ? Optional.Some(x.Value) : Optional.NoneOf<T>(whyNull.Or(MissingValueReason.ConvertedFromNull));
		}

		/// <summary>
		/// Converts an object to an <see cref="Optional{T}"/>, mapping a <c>null</c> to a None.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="x">The value.</param>
		/// <param name="whyNull"></param>
		/// <returns></returns>
		public static Optional<T> AsOptional<T>(this T x, Optional<object> whyNull = default(Optional<object>)) => 
			x != null ? x.AsOptionalSome() : Optional.NoneOf<T>(whyNull.Or(MissingValueReason.ConvertedFromNull));


		/// <summary>
		///     Returns the underlying value of the Optional, or throws an exception if none exists.
		/// </summary>
		/// <typeparam name="T">The inner type.</typeparam>
		/// <param name="optional">The Optional.</param>
		/// <param name="ex">The exception to throw if <paramref name="optional"/> is None.</param>
		/// <exception cref="Exception">Throws the given exception if <paramref name="optional"/> has no inner value.</exception>
		/// <returns></returns>
		public static T ValueOrError<T>(this Optional<T> optional, Exception ex) {
			if (!optional.HasValue) throw ex;
			return optional.Value;
		}

		/// <summary>
		/// Flattens an <see cref="Optional{T}"/> by converting a Some(null) into a None.
		/// </summary>
		/// <param name="optional">The Optional.</param>
		/// <param name="whyNull">A reason for why the internal object might be null.</param>
		/// <typeparam name="T">The inner type.</typeparam>
		/// <returns></returns>
		public static Optional<T> Flatten<T>(this Optional<T> optional, Optional<object> whyNull = default(Optional<object>)) where T : class {
			return optional.HasValue && optional.Value != null ? optional : Optional.NoneOf<T>(whyNull.Or(MissingValueReason.ConvertedFromNull));
		}

		/// <summary>
		/// Flattens a nested <see cref="Optional{T}"/>, returning the innermost value or None if no such value exists.
		/// </summary>
		/// <param name="optional">Then nested Optional.</param>
		/// <typeparam name="T">The innermost type.</typeparam>
		/// <returns></returns>
		public static Optional<T> Flatten<T>(this Optional<Optional<T>> optional) {
			return optional.HasValue ? optional.Value : Optional.NoneOf<T>(optional.Reason);
		}

		/// <summary>
		/// Flattens a nullable with an Optional inner type, turning a <c>null</c> into a None.
		/// </summary>
		/// <typeparam name="T">The inner type.</typeparam>
		/// <param name="maybeOptional">The nested Optional.</param>
		/// <param name="whyNull">Reason for why the original value might be null.</param>
		/// <returns></returns>
		public static Optional<T> Flatten<T>(this Optional<T>? maybeOptional, Optional<object> whyNull = default(Optional<object>)) {
			return maybeOptional ?? Optional.NoneOf<T>(whyNull.Or(MissingValueReason.ConvertedFromNull));
		}

		/// <summary>
		/// Flattens an Optional with a nullable inner type, turning Some(null) into a None.
		/// </summary>
		/// <typeparam name="T">The optional value type.</typeparam>
		/// <param name="maybeOptional">The nested optional value.</param>
		/// <param name="whyNull">Optionally, why the inner value is null.</param>
		/// <returns></returns>
		public static Optional<T> Flatten<T>(this Optional<T?> maybeOptional, Optional<object> whyNull = default(Optional<object>)) where T : struct {
			return !maybeOptional.HasValue ? Optional.NoneOf<T>(maybeOptional.Reason) : maybeOptional.Value.AsOptional(whyNull.Or(MissingValueReason.ConvertedFromNull));
		}

		/// <summary>
		/// Converts an Optional with a class inner type, turning a None into a <c>null</c>.
		/// </summary>
		/// <typeparam name="T">The inner type.</typeparam>
		/// <param name="optional">The Optional.</param>
		/// <returns></returns>
		public static T ToClass<T>(this Optional<T> optional) where T : class {
			return optional.HasValue ? optional.Value : null;
		}

		/// <summary>
		/// Converts an Optional with a struct inner type, returning a nullable struct and turning a None into a <c>null</c>.
		/// </summary>
		/// <typeparam name="T">The inner type.</typeparam>
		/// <param name="optional">The Optional.</param>
		/// <returns></returns>
		public static T? ToNullable<T>(this Optional<T> optional)
			where T : struct {
			return !optional.HasValue ? (T?)null : optional.Value;
		}

		/// <summary>
		/// Converts an Optional into a sequence of zero or one elements.
		/// </summary>
		/// <typeparam name="T">The inner type.</typeparam>
		/// <param name="optional">The Optional.</param>
		/// <returns></returns>
		public static IEnumerable<T> ToEnumerable<T>(this Optional<T> optional)
		{
			return optional.HasValue ? new[] {
				optional.Value
			} : new T[0];
		}
	}
}