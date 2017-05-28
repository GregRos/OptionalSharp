using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#pragma warning disable 618 //Obsolete warning about AnyNone

namespace OptionalSharp {

	/// <summary>
	///     A type that indicates an optional value of type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The type of value.</typeparam>
	[Serializable]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public partial struct Optional<T> : IEquatable<T>, IEquatable<Optional<T>>, IAnyOptional, IFormattable {
		private static readonly IEqualityComparer<T> _eq = EqualityComparer<T>.Default;
		private const int NoneHashCode = 0;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly bool _exists;

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		readonly T _value;

		public Optional(T value) : this() {
			_value = value;
			_exists = true;
		}
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object IAnyOptional.Value => Value;

		/// <summary>
		///     Returns an instance indicating a missing value.
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static Optional<T> None => new Optional<T>();

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		string DebuggerDisplay {
			get {
				if (Exists) return $"Some({Value})";
				return $"None";
			}
		}

		/// <summary>
		/// Returns true if this optional value is Some.
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool Exists => _exists;

		/// <summary>
		///		Returns the underlying value, or throws an exception if none exists.
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value {
			get {
				if (!Exists) throw Errors.NoValue<T>();
				return _value;
			}
		}

		/// <summary>
		///     Returns a string representation of this optional value. 
		/// </summary>
		/// <returns>
		///     A string that represents this optional value.
		/// </returns>
		public override string ToString() {
			return Exists && Value != null ? Value.ToString() : "";
		}

		public string ToString(string format, IFormatProvider formatProvider) {
			return !Exists ? "" : (Value as IFormattable)?.ToString(format, formatProvider) ?? "";
		}
	}

	
	/// <summary>
	/// Contains extension and utility methods for dealing with Optional values.
	/// </summary>
	public static class Optional {

		
		/// <summary>
		///     Returns a special token that can be implicitly converted to a None optional value instance of any type.
		/// </summary>
		public static AnyNoneToken None => AnyNoneToken.Instance;

		/// <summary>
		///     Returns an optional value instance indicating a missing value of type <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static Optional<T> NoneOf<T>() => None;

		/// <summary>
		///     Returns an optional value instance wrapping the specified value.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="value">The value to wrap.</param>
		/// <returns></returns>
		public static Optional<T> Some<T>(T value) => new Optional<T>(value);


	}


}