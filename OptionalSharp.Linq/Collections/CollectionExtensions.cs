using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace OptionalSharp.Linq
{
	public static class CollectionExtensions
	{
		public static Optional<TValue> TryGet<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key) {
			return @this.ContainsKey(key) ? @this[key].AsOptionalSome() : Optional.None;
		}

		public static Optional<T> TryGet<T>(this IList<T> @this, int index) {
			return @this.Count > index ? @this[index].AsOptionalSome() : Optional.None;
		}

		public static Optional<T> TryFirst<T>(this IEnumerable<T> @this) {
			using (var iter = @this.GetEnumerator()) {
				if (iter.MoveNext()) {
					return iter.Current.AsOptionalSome();
				}
				return Optional.None;
			}
		}

		public static Optional<T> TryFirst<T>(this IEnumerable<T> @this, Func<T, bool> predicate) {
			return @this.Where(predicate).TryFirst();
		}

		public static Optional<T> TryLast<T>(this IEnumerable<T> @this) {
			var list = @this.ToList();
			return list.Count == 0 ? Optional.None : list[list.Count - 1].AsOptionalSome();
		}

		public static Optional<T> TryElementAt<T>(this IEnumerable<T> @this, int index) {
			using (var iter = @this.GetEnumerator()) {
				while (iter.MoveNext() && index > 0) {
					index--;
				}
				return index > 0 ? Optional.None : iter.Current.AsOptionalSome();
			}
		}
			 
		public static Optional<T> TryLast<T>(this IEnumerable<T> @this, Func<T, bool> predicate) {
			return @this.Where(predicate).TryLast();
		}

		public static IEnumerable<TOut> Choose<T, TOut>(this IEnumerable<T> @this, Func<T, Optional<TOut>> selector) {
			return @this.Select(selector).Where(x => x.HasValue).Select(x => x.Value);
		}

		public static Optional<TOut> TryPick<T, TOut>(this IEnumerable<T> @this, Func<T, Optional<TOut>> selector) {
			return @this.Select(selector).TryFirst(x => x.HasValue).Flatten();
		}

		public static IEnumerable<T> Flatten<T>(this IEnumerable<Optional<T>> @this) {
			return @this.Where(x => x.HasValue).Select(x => x.Value);
		}



		public static OptionalTaskAwaiter<T> GetAwaiter<T>(this Optional<Task<T>> @this)
		{
			return new OptionalTaskAwaiter<T>(@this);
		}

		public class OptionalTaskAwaiter<T> : ICriticalNotifyCompletion
		{
			private readonly Optional<Task<T>> _inner;

			internal OptionalTaskAwaiter(Optional<Task<T>> inner)
			{
				_inner = inner;
			}

			public bool IsCompleted => !_inner.HasValue || _inner.Value.IsCompleted;

			public Optional<T> GetResult()
			{
				return !_inner.HasValue ? Optional.None : _inner.Value.GetAwaiter().GetResult().AsOptionalSome();
			}

			public void OnCompleted(Action continuation)
			{
				_inner.Value.ConfigureAwait(true).GetAwaiter().OnCompleted(continuation);
			}

			public void UnsafeOnCompleted(Action continuation)
			{
				_inner.Value.ConfigureAwait(true).GetAwaiter().UnsafeOnCompleted(continuation);
			}
		}
	}
}
