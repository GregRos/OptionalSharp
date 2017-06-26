#if NET4_5
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OptionalSharp.More {
	/// <summary>
	/// Extensions for Optionals with parallelism
	/// </summary>
	public static class ParallelExtensions {

		/// <summary>
		/// Returns the awaiter for the optional task. Used for await calls.
		/// </summary>
		/// <typeparam name="T">The inner type.</typeparam>
		/// <param name="this">The optional task.</param>
		/// <returns></returns>
		public static ParallelExtensions.OptionalTaskAwaiter<T> GetAwaiter<T>(this Optional<Task<T>> @this) {
			return new ParallelExtensions.OptionalTaskAwaiter<T>(@this);
		}
#pragma warning disable CS1591
		[CompilerGenerated]
		[EditorBrowsable(EditorBrowsableState.Never)]
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
				return !_inner.HasValue ? Optional.None() : _inner.Value.GetAwaiter().GetResult().AsOptionalSome();
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

#endif