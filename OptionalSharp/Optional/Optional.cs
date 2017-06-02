using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

#pragma warning disable 618 //Obsolete warning about AnyNone

namespace OptionalSharp {

	/// <summary>
	///    An Optional of inner type <typeparamref name="T"/>. Indicates a potentially missing value of that type.
	/// </summary>
	/// <typeparam name="T">The inner type, or the type of the underlying value.</typeparam>
	[Serializable]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public partial struct Optional<T> : IEquatable<Optional<T>>, IAnyOptional, IFormattable {
		private static readonly IEqualityComparer<T> _eq = EqualityComparer<T>.Default;
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly bool _hasValue;

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		readonly T _value;

		/// <summary>
		/// Constructs a Some-state Optional, which means it contains an inner value.
		/// </summary>
		/// <param name="value">The inner value of the Optional.</param>
		public Optional(T value) : this() {
			_value = value;
			_hasValue = true;
		}
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object IAnyOptional.Value => Value;

		/// <summary>
		/// Gets the inner type of this Optional.
		/// </summary>
		/// <returns></returns>
		public Type GetInnerType() {
			return typeof(T);
		}

		/// <summary>
		///     Returns an instance indicating a missing value.
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static Optional<T> None => new Optional<T>();

		/// <summary>
		/// Returns an informational string describing the object with more detail than <see cref="ToString"/>.
		/// </summary>
		/// <returns></returns>
		public string ToDebugString() {
			if (HasValue) return $"Some<{typeof(T).PrettyName()}>({Value})";
			return $"NoneOf<{typeof(T).PrettyName()}>";
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		string DebuggerDisplay {
			get {
				return ToDebugString();
			}
		}

		/// <summary>
		/// Returns true if this Optional is in its Some state, i.e. if it has an inner value.
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool HasValue => _hasValue;

		/// <summary>
		///		Returns the inner value, or throws an exception if none exists.
		/// </summary>
		/// <exception cref="MissingOptionalValueException">Thrown if no inner value exists, i.e. this Optional is in its None state.</exception>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value {
			get {
				if (!HasValue) throw Errors.NoValue<T>();
				return _value;
			}
		}

		/// <summary>
		///     Returns a string representation of this Optional, usually that of its inner value.
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
		///     Returns a special Optional token in a None state, without a known inner type. This token can be implicitly converted to any <see cref="Optional{T}"/>.
		/// </summary>
		public static ImplicitNoneValue None => ImplicitNoneValue.Instance;

		/// <summary>
		///     Returns an <see cref="Optional{T}"/> in its None state, i.e. without an inner value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static Optional<T> NoneOf<T>() => None;

		/// <summary>
		///     Returns an <see cref="Optional{T}"/> in its Some state, i.e. with an inner value.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="value">The value to wrap.</param>
		/// <returns></returns>
		public static Optional<T> Some<T>(T value) => new Optional<T>(value);

		/// <summary>
		/// Creates a new <see cref="Optional{T}"/> at runtime, with its inner type being the runtime type of the passed value.
		/// </summary>
		/// <param name="any">The inner value. If <c>null</c>, its inner type will be considered <see cref="object"/>.</param>
		/// <param name="typeOverride">Optionally, the explicit inner type of the resulting <see cref="Optional{T}"/>. Must be compatible with the value.</param>
		/// <returns></returns>
		public static IAnyOptional RuntimeCreateSome(object any, Optional<Type> typeOverride = default(Optional<Type>))
		{
			var innerType = any?.GetType() ?? typeof(object);
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

		/// <summary>
		/// Creates a new <see cref="Optional{T}"/> at runtime, in its None state, with the appropriate inner type.
		/// </summary>
		/// <param name="type">The inner type of the Optional.</param>
		/// <returns></returns>
		public static IAnyOptional RuntimeCreateNone(Type type) {
			if (type == null) throw Errors.ArgumentNull("type");
			var optional = typeof(Optional<>).MakeGenericType(type);

			var result = Activator.CreateInstance(optional);
			return (IAnyOptional) result;
		}
	}

}