using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using static OptionalSharp.Optional;
namespace OptionalSharp.Linq {
	/// <summary>
	/// Extension methods for <see cref="IEnumerable{T}"/> objects that utilize Optionals, or the other way around.
	/// </summary>
	public static class EnumerableExtensions {
		/// <summary>
		/// Tries to return the first element of a sequence, returning None if the sequence is empty.
		/// </summary>
		/// <typeparam name="T">The type of the element.</typeparam>
		/// <param name="this">The sequence.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		public static Optional<T> TryFirst<T>(this IEnumerable<T> @this) {
			if (@this == null) throw Errors.ArgumentNull(nameof(@this));
			using (var iter = @this.GetEnumerator()) {
				if (iter.MoveNext()) {
					return iter.Current.AsOptionalSome();
				}
				return None(MissingReasons.CollectionWasEmpty);
			}
		}

		/// <summary>
		/// When invoked on a dictionary or a sequence of key-value pairs, tries to get the first (or only) value associated with the given key.
		/// If no key was found, returns None.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="this">The dictionary or sequence of key-value pairs.</param>
		/// <param name="key">The key to look up.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		public static Optional<TValue> TryGet<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> @this, TKey key) {
			if (@this == null) throw Errors.ArgumentNull(nameof(@this));
			bool success;
			TValue v;
			if (@this is IDictionary<TKey, TValue> id) {
				success = id.TryGetValue(key, out v);
			}
#if NET4_0
			else if (@this is IReadOnlyDictionary<TKey, TValue> id2) {
				success = id2.TryGetValue(key, out v);
			}
#endif
			else {
				var kEq = EqualityComparer<TKey>.Default;
				var fst = @this.TryFirst(kvp => kEq.Equals(kvp.Key, key));
				return fst.Select(x => x.Value);
			}
			return success ? Optional.Some(v) : Optional.None(MissingReasons.KeyNotFound);
		}

		/// <summary>
		/// Tries to return the first element in the sequence matching the given predicate. If no element was found, returns None.
		/// </summary>
		/// <typeparam name="T">The type of the element.</typeparam>
		/// <param name="this">The sequence or collection.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		public static Optional<T> TryFirst<T>(this IEnumerable<T> @this, Func<T, bool> predicate) {
			if (@this == null) throw Errors.ArgumentNull(nameof(@this));
			if (predicate == null) throw Errors.ArgumentNull(nameof(predicate));
			return @this.Where(predicate).TryFirst().WithReason(MissingReasons.NoElementsFound);
		}
		/// <summary>
		/// Tries to return the last element in the sequence. If the sequence is empty, returns None.
		/// </summary>
		/// <typeparam name="T">The type of the element.</typeparam>
		/// <param name="this">The sequence or collection.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		public static Optional<T> TryLast<T>(this IEnumerable<T> @this) {
			if (@this == null) throw Errors.ArgumentNull(nameof(@this));
			var list = @this.ToList();
			return list.Count == 0 ? None(MissingReasons.CollectionWasEmpty) : list[list.Count - 1].AsOptionalSome();
		}

		/// <summary>
		/// Tries to return the element at the given index in the list or sequence. If the given index is outside the bounds of the collection, returns None.
		/// </summary>
		/// <typeparam name="T">The type of the element.</typeparam>
		/// <param name="this">The list or sequence.</param>
		/// <param name="index">The index.</param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if the index is negative.</exception>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		/// <returns></returns>
		public static Optional<T> TryElementAt<T>(this IEnumerable<T> @this, int index) {
			if (@this == null) throw Errors.ArgumentNull(nameof(@this));
			if (index < 0) throw Errors.ArgumentCannotBeNegative(nameof(index), index);
			if (@this is IList<T> il) {
				return il.Count > index ? Some(il[index]) : None(MissingReasons.IndexNotFound);
			}
#if NET4_0
			if (@this is IReadOnlyList<T> il2) {
				return il2.Count > index ? Some(il2[index]) : None(MissingReasons.IndexNotFound);
			}
#endif
			using (var iter = @this.GetEnumerator()) {
				while (iter.MoveNext() && index > 0) {
					index--;
				}
				return index > 0 ? Optional.None(MissingReasons.IndexNotFound) : iter.Current.AsOptionalSome();
			}
		}

		/// <summary>
		/// Tries to return the last element in the list or sequence that matches the given predicate. If no element is found, returns None.
		/// </summary>
		/// <typeparam name="T">The type of the element.</typeparam>
		/// <param name="this">The sequence or list.</param>
		/// <param name="predicate">The predicate.</param>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		/// <returns></returns>
		public static Optional<T> TryLast<T>(this IEnumerable<T> @this, Func<T, bool> predicate) {
			if (@this == null) throw Errors.ArgumentNull(nameof(@this));
			if (predicate == null) throw Errors.ArgumentNull(nameof(predicate));
			return @this.Where(predicate).TryLast().WithReason(MissingReasons.NoElementsFound);
		}

		/// <summary>
		/// Projects every element in the sequence using the given projection function, filtering out all None results.
		/// </summary>
		/// <typeparam name="T">The type of the input element.</typeparam>
		/// <typeparam name="TOut">The type of the output element.</typeparam>
		/// <param name="this">The input sequence.</param>
		/// <param name="selector">The projection selector. A result of None indicates to skip the element.</param>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		/// <returns></returns>
		public static IEnumerable<TOut> Choose<T, TOut>(this IEnumerable<T> @this, Func<T, Optional<TOut>> selector) {
			if (@this == null) throw Errors.ArgumentNull(nameof(@this));
			if (selector == null) throw Errors.ArgumentNull(nameof(selector));
			return @this.Select(selector).Where(x => x.HasValue).Select(x => x.Value);
		}

		/// <summary>
		/// Applies the given selector on every element in the sequence, returning the first result that has an inner value. Returns None if no such result exists. 
		/// </summary>
		/// <typeparam name="T">The type of the input element.</typeparam>
		/// <typeparam name="TOut">The type of the result.</typeparam>
		/// <param name="this">The sequence.</param>
		/// <param name="selector">The selector. Only a Some result is considered positive.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		public static Optional<TOut> TryPick<T, TOut>(this IEnumerable<T> @this, Func<T, Optional<TOut>> selector) {
			if (@this == null) throw Errors.ArgumentNull(nameof(@this));
			if (selector == null) throw Errors.ArgumentNull(nameof(selector));
			return @this.Select(selector).TryFirst(x => x.HasValue).Flatten().WithReason(MissingReasons.NoElementsFound);
		}

		/// <summary>
		/// Projects the inner value of an <see cref="Optional{T}"/> into a sequence and returns that sequence, or returns an empty sequence if there is no inner value.
		/// </summary>
		/// <typeparam name="T">The type of the inner value.</typeparam>
		/// <typeparam name="TOut">The type of the sequence element.</typeparam>
		/// <param name="this">The Optional.</param>
		/// <param name="selector">The projection.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		public static IEnumerable<TOut> SelectMany<T, TOut>(this Optional<T> @this, Func<T, IEnumerable<TOut>> selector) {
			if (selector == null) throw Errors.ArgumentNull(nameof(selector));
			return @this.Select(selector).Or(new TOut[0]);
		}

		/// <summary>
		/// Projects every element of a sequence into an <see cref="IEnumerable{T}"/> of <see cref="Optional{T}"/>, then flattens the sequence by filtering out None instances.
		/// </summary>
		/// <typeparam name="T">The type of the input sequence.</typeparam>
		/// <typeparam name="TOut">The type of the output sequence.</typeparam>
		/// <param name="this">The sequence.</param>
		/// <param name="selector">The selector.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		public static IEnumerable<TOut> ChooseMany<T, TOut>(
			this IEnumerable<T> @this, Func<T, IEnumerable<Optional<TOut>>> selector) {
			if (@this == null) throw Errors.ArgumentNull(nameof(@this));
			if (selector == null) throw Errors.ArgumentNull(nameof(selector));

			return @this.SelectMany(x => selector(x).Flatten());
		}

		/// <summary>
		/// Flattens an <see cref="IEnumerable{T}"/> of <see cref="Optional{T}"/> by filtering out None instances.
		/// </summary>
		/// <typeparam name="T">The element type.</typeparam>
		/// <param name="this">The sequence.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		public static IEnumerable<T> Flatten<T>(this IEnumerable<Optional<T>> @this) {
			if (@this == null) throw Errors.ArgumentNull(nameof(@this));

			return @this.Where(x => x.HasValue).Select(x => x.Value);
		}

		/// <summary>
		/// Flattens a <see cref="Optional{T}"/> of <see cref="IEnumerable{T}"/> by returning the inner value, or else an empty sequence if none exists.
		/// </summary>
		/// <typeparam name="T">The type of the element.</typeparam>
		/// <param name="this">The sequence.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		public static IEnumerable<T> Flatten<T>(this Optional<IEnumerable<T>> @this) {
			if (@this == null) throw Errors.ArgumentNull(nameof(@this));

			return @this.Or(new T[0]);
		}
		/// <summary>
		/// Returns the single element in the sequence that fulfills the given predicate, or else None if no such element exists. Throws an exception if more than one element is found.
		/// </summary>
		/// <typeparam name="T">The type of the element.</typeparam>
		/// <param name="this">The sequence.</param>
		/// <param name="predicate">The predicate.</param>
		/// <exception cref="ArgumentException">Thrown if the sequence contains more than one element matching the predicate.</exception>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		/// <returns></returns>
		public static Optional<T> TrySingle<T>(this IEnumerable<T> @this, Func<T, bool> predicate) {
			if (@this == null) throw Errors.ArgumentNull(nameof(@this));
			if (predicate == null) throw Errors.ArgumentNull(nameof(predicate));

			return @this.Where(predicate).TrySingle();
		}

		/// <summary>
		/// Returns the single element in the sequence, or else None if the sequence is empty. Throws an exception if the sequence has more than one element.
		/// </summary>
		/// <typeparam name="T">The type of the element.</typeparam>
		/// <param name="this">The sequence.</param>
		/// <exception cref="ArgumentException">Thrown if the sequence contains more than one element.</exception>
		/// <exception cref="ArgumentNullException">An argument was null.</exception>
		/// <returns></returns>
		public static Optional<T> TrySingle<T>(this IEnumerable<T> @this) {
			if (@this == null) throw Errors.ArgumentNull(nameof(@this));

			using (var iter = @this.GetEnumerator())
			{
				if (!iter.MoveNext()) return Optional.None(MissingReasons.NoElementsFound);
				var val = iter.Current;
				if (iter.MoveNext()) throw Errors.ExpectedNoMoreThanOneElement();
				return val;
			}
		}			
	}
}