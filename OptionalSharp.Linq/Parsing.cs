using System;
using System.ComponentModel;
using System.Globalization;
using static OptionalSharp.Optional;
using SystemDateTime = System.DateTime;

namespace OptionalSharp.Linq {
	/// <summary>
	/// Static class containing methods for utilizing <see cref="Optional{T}"/> for parsing common types.
	/// </summary>
	public static class TryParse {
		/// <summary>
		/// Static class containing methods for parsing <see cref="System.DateTime"/>.
		/// </summary>
		public static class DateTime {
			/// <summary>
			/// Parses a string as a date using exact parsing information. 
			/// See <seealso cref="SystemDateTime.ParseExact(string, string, IFormatProvider, DateTimeStyles)"/>.
			/// </summary>
			/// <param name="value">The string to parse.</param>
			/// <param name="format">A format specifier.</param>
			/// <param name="provider">The format provider to use.</param>
			/// <param name="styles">The styles to use.</param>
			/// <returns></returns>
			public static Optional<SystemDateTime> Exact(
				string value, string format, IFormatProvider provider = null, DateTimeStyles styles =  DateTimeStyles.AssumeLocal) {

				return SystemDateTime.TryParseExact(value, format, provider, styles, out var x) ? Some(x)
					: None(MissingReasons.CouldNotBeParsedAs<SystemDateTime>.Value);
			}

			/// <summary>
			/// Parses a string as a date using implicit parsing information.
			/// See <seealso cref="SystemDateTime.Parse(string, IFormatProvider, DateTimeStyles)"/>.
			/// </summary>
			/// <param name="value">The string to parse.</param>
			/// <param name="provider">The provider to use.</param>
			/// <param name="styles">The styles to use.</param>
			/// <returns></returns>
			public static Optional<SystemDateTime> Loose(
				string value, IFormatProvider provider = null, DateTimeStyles styles = DateTimeStyles.AssumeLocal) {
				return SystemDateTime.TryParse(value, provider, styles, out var x) ? Some(x)
					: None(MissingReasons.CouldNotBeParsedAs<SystemDateTime>.Value);
			}
		}

		/// <summary>
		/// Tries to parse a <see cref="System.Int32"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse</param>
		/// <returns></returns>
		public static Optional<int> Int32(string value) {
			return int.TryParse(value, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<int>.Value);
		}

		/// <summary>
		/// Tries to parse a <see cref="System.Int16"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse</param>
		/// <returns></returns>
		public static Optional<short> Int16(string value) {
			return short.TryParse(value, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<short>.Value);
		}
		/// <summary>
		/// Tries to parse a <see cref="System.Single"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse</param>
		/// <returns></returns>
		public static Optional<float> Float(string value) {
			return float.TryParse(value, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<float>.Value);
		}
		/// <summary>
		/// Tries to parse a <see cref="System.Double"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse</param>
		/// <returns></returns>
		public static Optional<double> Double(string value) {
			return double.TryParse(value, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<double>.Value);
		}
		/// <summary>
		/// Tries to parse a <see cref="System.Decimal"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse</param>
		/// <returns></returns>
		public static Optional<decimal> Decimal(string value) {
			return decimal.TryParse(value, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<decimal>.Value);
		}
		/// <summary>
		/// Tries to parse a <see cref="System.UInt16"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse</param>
		/// <returns></returns>
		public static Optional<ushort> UInt16(string value) {
			return ushort.TryParse(value, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<ushort>.Value);
		}
		/// <summary>
		/// Tries to parse a <see cref="System.UInt32"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse</param>
		/// <returns></returns>
		public static Optional<uint> UInt32(string value) {
			return uint.TryParse(value, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<uint>.Value);
		}
		/// <summary>
		/// Tries to parse a <see cref="System.UInt64"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse</param>
		/// <returns></returns>
		public static Optional<ulong> UInt64(string value) {
			return ulong.TryParse(value, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<ulong>.Value);
		}
		/// <summary>
		/// Tries to parse a <see cref="System.Int64"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse</param>
		/// <returns></returns>
		public static Optional<long> Int64(string value) {
			return long.TryParse(value, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<long>.Value);
		}
		/// <summary>
		/// Tries to parse a <see cref="System.Byte"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse</param>
		/// <returns></returns>
		public static Optional<byte> Byte(string value) {
			return byte.TryParse(value, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<byte>.Value);
		}
		/// <summary>
		/// Tries to parse a <see cref="System.SByte"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse</param>
		/// <returns></returns>
		public static Optional<sbyte> SByte(string value) {
			return sbyte.TryParse(value, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<sbyte>.Value);
		}
		/// <summary>
		/// Tries to parse a <see cref="System.Char"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse</param>
		/// <returns></returns>
		public static Optional<char> Char(string value) {
			return char.TryParse(value, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<char>.Value);
		}
		/// <summary>
		/// Tries to parse a <see cref="System.Boolean"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse</param>
		/// <returns></returns>
		public static Optional<bool> Bool(string value) {
			return bool.TryParse(value, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<bool>.Value);
		}
		
		/// <summary>
		/// Tries to parse an enum of type <typeparamref name="TEnum"/>, returning None if the parsing fails.
		/// </summary>
		/// <typeparam name="TEnum">The type to parse the string as. Must be an enum type.</typeparam>
		/// <param name="value">The string to parse.</param>
		/// <param name="ignoreCase">Whether to parse in a case-insensitive manner.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">The type <typeparamref name="TEnum"/> is not an <see cref="System.Enum"/> type.</exception>
		public static Optional<TEnum> Enum<TEnum>(string value, bool ignoreCase = false)
			where TEnum : struct {
			if (!typeof(TEnum).IsEnum) {
				throw Errors.EnumExpected(typeof(TEnum));
			}
			return System.Enum.TryParse<TEnum>(value, ignoreCase, out var x) ? Some(x) : None(MissingReasons.CouldNotBeParsedAs<TEnum>.Value);
		}
		
		/// <summary>
		/// Tries to parse a string as a value of type <typeparamref name="T"/>, where <typeparamref name="T"/> must be a paraseable type.
		/// </summary>
		/// <typeparam name="T">The type to parse the string as. Must be an integral type, <see cref="DateTime"/>, or an enum.</typeparam>
		/// <param name="value">The string to parse.</param>
		/// <exception cref="ArgumentException">Cannot parse strings as <typeparamref name="T"/>.</exception>
		/// <returns></returns>
		public static Optional<T> Dynamically<T>(string value)
			where T : struct, IFormattable {
			var t = typeof(T);
			if (t == typeof(int)) {
				return (T) (object) Int32(value);
			}
			if (t == typeof(long)) {
				return (T) (object) Int64(value);
			}
			if (t == typeof(float)) {
				return (T) (object) Float(value);
			}
			if (t == typeof(double)) {
				return (T) (object) Double(value);
			}
			if (t == typeof(decimal)) {
				return (T) (object) Decimal(value);
			}
			if (t == typeof(bool)) {
				return (T) (object) Bool(value);
			}
			if (t == typeof(byte)) {
				return (T) (object) Byte(value);
			}
			if (t == typeof(sbyte)) {
				return (T) (object) SByte(value);
			}
			if (t == typeof(short)) {
				return (T) (object) Int16(value);
			}
			if (t == typeof(uint)) {
				return (T) (object) UInt32(value);
			}
			if (t == typeof(ulong)) {
				return (T) (object) UInt64(value);
			}
			if (t == typeof(ushort)) {
				return (T) (object) UInt16(value);
			}
			if (t == typeof(char)) {
				return (T) (object) Char(value);
			}
			if (t == typeof(DateTime)) {
				return (T) (object) DateTime.Loose(value);
			}
			if (t.IsEnum) {
				return (T) (object) Enum<T>(value);
			}
			if (t == typeof(string) || t.IsAssignableFrom(typeof(string))) {
				return (T)(object)value;
			}

			throw Errors.InvalidParseType(typeof(T));
		}
	}
}
