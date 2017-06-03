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
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public struct ImplicitNoneValue : IAnyOptional, IFormattable, IEquatable<IAnyOptional> {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly object _reason;

		/// <summary>
		/// An object describing the reason for the lack of a value.
		/// </summary>
		public object Reason => _reason ?? MissingValueReason.NoReasonSpecified;

		internal ImplicitNoneValue(object reason) {
			_reason = reason;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		string DebuggerDisplay => ToDebugString();

		/// <summary>
		/// Returns an informational string that describes the object.
		/// </summary>
		/// <returns></returns>
		public string ToDebugString() {
			return "NoneOf<*>";
		}

		/// <summary>
		/// Returns false.
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool HasValue => false;

		/// <summary>
		/// Throws a <see cref="MissingOptionalValueException"/>.
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public object Value => throw Errors.NoValue(typeof(object), _reason);
		
		/// <summary>
		/// Checks if this object is equal to another object.
		/// </summary>
		/// <param name="obj">The second object.</param>
		/// <returns></returns>
		public override bool Equals(object obj) {
			return obj is IAnyOptional i && !i.HasValue;
		}
		/// <summary>
		/// Gets the hash code of the object.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode() {
			return OptionalShared.NoneHashCode;
		}

		/// <summary>
		/// Converts this None instance to a specific <see cref="Optional{T}"/> instance representing a missing value of a specific type.
		/// </summary>
		/// <typeparam name="T">The inner type of the resulting Optional.</typeparam>
		/// <returns></returns>
		public Optional<T> Cast<T>() {
			return new Optional<T>(default(T), false, _reason);
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

		/// <summary>
		/// Gets the inner type of this Optional
		/// </summary>
		/// <returns></returns>
		public Type GetInnerType() {
			return typeof(object);
		}

	}
}