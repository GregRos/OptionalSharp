using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

#pragma warning disable 618 //Obsolete warning about AnyNone

namespace OptionalSharp {

	/// <summary>
	///     Indicates an optional value of type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The type of value.</typeparam>
	[Serializable]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public partial struct Optional<T> : IEquatable<T>, IEquatable<Optional<T>>, IAnyOptional, IFormattable {
		private static readonly IEqualityComparer<T> _eq = EqualityComparer<T>.Default;
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly bool _hasValue;

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		readonly T _value;

		public Optional(T value) : this() {
			_value = value;
			_hasValue = true;
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
				if (HasValue) return $"Some({Value})";
				return $"None";
			}
		}

		/// <summary>
		/// Returns true if this optional value is Some.
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool HasValue => _hasValue;

		/// <summary>
		///		Returns the underlying value, or throws an exception if none exists.
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value {
			get {
				if (!HasValue) throw Errors.NoValue<T>();
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
			return HasValue && Value != null ? Value.ToString() : "";
		}

		string IFormattable.ToString(string format, IFormatProvider formatProvider) {
			return !HasValue ? "" : (Value as IFormattable)?.ToString(format, formatProvider) ?? "";
		}
	}

	
	/// <summary>
	/// Contains extension and utility methods for dealing with Optional values.
	/// </summary>
	public static class Optional {

		
		/// <summary>
		///     Returns a special token that can be implicitly converted to a None optional value instance of any type.
		/// </summary>
		public static ImplicitNoneValue None => ImplicitNoneValue.Instance;

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

		/// <summary>
		/// Wraps a value as a strongly-typed Optional at runtime, when no static type of the value is known.
		/// </summary>
		/// <param name="any">The inner value.</param>
		/// <param name="typeOverride">Optionally, the value type parameter of the resulting Optional, used instead of the value's runtime type.</param>
		/// <returns></returns>
		public static IAnyOptional CreateRuntimeOptional(object any, Optional<Type> typeOverride = default(Optional<Type>))
		{
			if (any == null) throw Errors.ArgumentNull(nameof(any));
			var innerType = any.GetType();
			if (typeOverride.HasValue)
			{
				if (!typeOverride.Value.IsAssignableFrom(innerType)) throw Errors.InvalidType(nameof(typeOverride));
				innerType = typeOverride.Value;
			}
			var optional = typeof(Optional<>).MakeGenericType(innerType);
			var ctor = optional.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly, null, new[] {
				innerType
			}, null);
			var result = ctor.Invoke(new[] { any });
			return (IAnyOptional)result;
		}
	}

}