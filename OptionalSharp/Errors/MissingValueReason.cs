using System.Diagnostics;

namespace OptionalSharp {
	internal static class MissingValueReason {
		internal static class FailedCast<TFrom, TTo> {
			public static readonly string Reason = $"Could not cast from {typeof(TFrom).PrettyName()} to type {typeof(TTo).PrettyName()}";
		}

		public static readonly string FailedFilter = "Failed filter without a specified reason.";

		public static readonly string ConvertedFromNull = "The missing value was converted from null.";

		public static readonly string NoReasonSpecified = "No reason has been specified.";

	}
}