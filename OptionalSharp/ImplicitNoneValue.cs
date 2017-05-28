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
	public struct ImplicitNoneValue : IAnyOptional, IFormattable, IEquatable<IAnyOptional> {
		/// <summary>
		/// Returns the instance of AnyNone.
		/// </summary>
		internal static readonly ImplicitNoneValue Instance = new ImplicitNoneValue();


		/// <summary>
		/// Returns false.
		/// </summary>
		public bool HasValue => false;

		/// <summary>
		/// Throws a <see cref="MissingOptionalValueException"/>.
		/// </summary>
		public object Value => throw Errors.NoValue();

		public override bool Equals(object obj) {
			return obj is IAnyOptional i && !i.HasValue;
		}

		public override int GetHashCode() {
			return Optional<int>.NoneHashCode;
		}

		string IFormattable.ToString(string format, IFormatProvider formatProvider) {
			return "";
		}
		/// <summary>
		/// Determines if the other IAnyOptional represents a non
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(IAnyOptional other) {
			return other?.HasValue == false;
		}
		/// <summary>
		/// Returns an empty string.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return "";
		}


	}
}