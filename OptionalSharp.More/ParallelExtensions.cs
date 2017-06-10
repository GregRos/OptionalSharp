namespace OptionalSharp.More {
	public static class ParallelExtensions {
#if NET4_5
		public static ParallelExtensions.OptionalTaskAwaiter<T> GetAwaiter<T>(this Optional<Task<T>> @this) {
			return new ParallelExtensions.OptionalTaskAwaiter<T>(@this);
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
#endif
	}
}