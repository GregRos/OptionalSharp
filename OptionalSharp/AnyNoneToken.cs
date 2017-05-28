using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace OptionalSharp {
	/// <summary>
	///     Normally hidden. Used to indicate a generic None value that can be converted to a typed None. Also acts as a None value where the value type is unknown.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	[CompilerGenerated]
	public class AnyNoneToken : IAnyOptional {
		/// <summary>
		/// Returns the instance of AnyNone.
		/// </summary>
		internal static readonly AnyNoneToken Instance = new AnyNoneToken();

		AnyNoneToken() {}

		/// <summary>
		/// Returns false.
		/// </summary>
		public bool Exists => false;

		/// <summary>
		/// Returns true.
		/// </summary>
		public bool IsNone => true;

		/// <summary>
		/// Throws a <see cref="NoValueException"/>.
		/// </summary>
		public object Value => throw new NoValueException();
	}
}