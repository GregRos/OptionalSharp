using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Schema;
namespace OptionalSharp {
	/// <summary>
	///     Static class with utility and extension methods for optional values.
	/// </summary>
	public static class OptionalExtensions {

		public static Optional<T> MakeSome<T>(this T @this) {
			return new Optional<T>(@this);
		}

		/// <summary>
		/// Converts the specified nullable value to an optional value. Null is represented as None.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="x">The value.</param>
		/// <returns></returns>
		public static Optional<T> AsOptional<T>(this T? x)
			where T : struct {
			return x.HasValue ? Optional.Some(x.Value) : Optional.None;
		}

		/// <summary>
		/// Converts the specified value to an optional type. Null is represented as None.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="x">The value.</param>
		/// <returns></returns>
		public static Optional<T> AsOptional<T>(this T x) => x != null ? x.MakeSome() : Optional.None;

		/// <summary>
		///     Returns the underlying value of the optional value instance, or throws an exception if none exists.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="opt"></param>
		/// <param name="ex"></param>
		/// <returns></returns>
		public static T ValueOrError<T>(this Optional<T> opt, Exception ex) {
			if (!opt.Exists) throw ex;
			return opt.Value;
		}

		/// <summary>
		///		Applies the ToString method on the underlying value, if one exists, and wraps the result in an optional value. Otherwise, returns None.
		/// </summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="optional">The optional value on which the method is invoked. </param>
		/// <returns></returns>
		public static Optional<string> MapString<T>(this Optional<T> optional) {
			return optional.Map(v => v.ToString());
		}

		/// <summary>
		///     Applies the ToString method on the inner value, using the specified IFormatProvider, and wraps the result in an optional value. Otherwise, returns None.
		/// </summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="optional">The optional value on which the method is invoked.</param>
		/// <param name="provider">The format provider, used as a parameter when calling ToString on the underlying value (if any).</param>
		/// <returns></returns>
		public static Optional<string> MapString<T>(this Optional<T> optional, IFormatProvider provider)
			where T : IConvertible {
			return optional.Map(v => v?.ToString(provider));
		}

		/// <summary>
		///     Compares an optional value instance to a concrete value. If an underlying value exists, compares it to the specified value. A missing value is smaller than any concrete value.
		/// </summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="optional">The optional value which is compared to the other value.</param>
		/// <param name="other">The other, concrete value.</param>
		/// <returns></returns>
		public static int CompareTo<T>(this Optional<T> optional, T other)
			where T : IComparable<T> {
			var comparer = Comparer<T>.Default;
			return !optional.Exists ? -1 : comparer.Compare(optional.Value, other);
		}

		/// <summary>
		///     Compares against another optional value instance, by comparing the underlying values (if those exist). A missing value is always smaller than an existing value. 
		/// </summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="optional">The first optional value, which is compared to the other optional value.</param>
		/// <param name="other">The second optional value.</param>
		/// <returns></returns>
		public static int CompareTo<T>(this Optional<T> optional, Optional<T> other)
			where T : IComparable<T> {
			return other.Exists ? optional.CompareTo(other.Value) : !optional.Exists ? 0 : 1;
		}

		/// <summary>
		/// Flattens an optional type wrapping a reference type by returning None if the underlying value is null.
		/// </summary>
		/// <param name="optional"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static Optional<T> Flatten<T>(this Optional<T> optional) where T : class {
			return optional.Exists && optional.Value != null ? optional : Optional.None;
		}


		/// <summary>
		/// Flattens a nested optional type, returning either the final underlying value, or None if no such value exists.
		/// </summary>
		/// <param name="optional"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static Optional<T> Flatten<T>(this Optional<Optional<T>> optional) {
			return optional.Exists ? optional.Value : Optional.None;
		}

		/// <summary>
		/// Flattens a nullable optional type, returning None if it is null, or if the underlying value doesn't exist.
		/// </summary>
		/// <typeparam name="T">The optional value type.</typeparam>
		/// <param name="maybeOptional">The nested optional value.</param>
		/// <returns></returns>
		public static Optional<T> Flatten<T>(this Optional<T>? maybeOptional) {
			return maybeOptional ?? Optional<T>.None;
		}

		/// <summary>
		/// Flattens an optional of a nullable type, returning None if it is null, or if the underlying value doesn't exist.
		/// </summary>
		/// <typeparam name="T">The optional value type.</typeparam>
		/// <param name="maybeOptional">The nested optional value.</param>
		/// <returns></returns>
		public static Optional<T> Flatten<T>(this Optional<T?> maybeOptional) where T : struct {
			return !maybeOptional.Exists ? Optional.NoneOf<T>() : maybeOptional.Value.AsOptional();
		}

		public static IEnumerable<T> Flatten<T>(this Optional<IEnumerable<T>> self) {
			return self.Exists ? self.Value : new T[0];
		}

		public static T ToClass<T>(this Optional<T> self) where T : class {
			return self.Exists ? self.Value : null;
		}

		public static T? ToNullable<T>(this Optional<T> self)
			where T : struct {
			return self.Exists ? (T?)null : self.Value;
		}

		public static List<T> ToList<T>(this Optional<T> self) {
			return self.Exists ? new List<T> {
				self.Value
			} : new List<T>();
		}

		public static T[] ToArray<T>(this Optional<T> self) {
			return self.Exists ? new[] {
				self.Value
			} : new T[0];
		}
		
		public static IEnumerable<T> ToEnumerable<T>(this Optional<T> self) {
			return self.Exists ? new[] {self.Value} : new T[0];
		}

		public static OptionalTaskAwaiter<T> GetAwaiter<T>(this Optional<Task<T>> @this) {
			return new OptionalTaskAwaiter<T>(@this);
		}

		public class OptionalTaskAwaiter<T> : ICriticalNotifyCompletion {
			private readonly Optional<Task<T>> _inner;

			internal OptionalTaskAwaiter(Optional<Task<T>> inner) {
				_inner = inner;
			}

			public bool IsCompleted => !_inner.Exists || _inner.Value.IsCompleted;

			public Optional<T> GetResult() {
				return !_inner.Exists ? Optional.None : _inner.Value.GetAwaiter().GetResult().MakeSome();
			}

			public void OnCompleted(Action continuation) {
				_inner.Value.ConfigureAwait(true).GetAwaiter().OnCompleted(continuation);
			}

			public void UnsafeOnCompleted(Action continuation) {
				_inner.Value.ConfigureAwait(true).GetAwaiter().UnsafeOnCompleted(continuation);
			}
		}
	
	}
}