using System.Diagnostics;

namespace OptionalSharp {
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal class MissingValueReason {
		public MissingValueReason(string reason) {
			Reason = reason;
		}
		
		public string Reason { get; }

		internal static class FailedCast<TFrom, TTo> {
			public static readonly MissingValueReason Reason = new MissingValueReason($"Could not cast from {typeof(TFrom).PrettyName()} to type {typeof(TTo).PrettyName()}");
		}

		public static readonly MissingValueReason FailedFilter = new MissingValueReason("Failed filter without a specified reason.");

		public static readonly MissingValueReason ConvertedFromNull = new MissingValueReason("The missing value was converted from null.");

		public static readonly MissingValueReason NoReasonSpecified = new MissingValueReason("No reason has been specified.");

		string DebuggerDisplay => ToString();

		public override string ToString() {
			return Reason;
		}
	}
}