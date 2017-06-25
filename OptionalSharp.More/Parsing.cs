using System;
using System.Globalization;
using SystemDateTime = System.DateTime;
using static OptionalSharp.Optional;
namespace OptionalSharp.More {
	/// <summary>
	/// Static class containing methods for utilizing <see cref="Optional{T}"/> for parsing common types.
	/// </summary>
	public static class TryParse {

		/// <summary>
		/// Parses a string as a date using exact parsing information. 
		/// See <seealso cref="SystemDateTime.ParseExact(string, string, IFormatProvider, DateTimeStyles)"/>.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="format">A format specifier. If <c>null</c>, TryParse is invoked. Otherwise, TryParseExact is invoked.</param>
		/// <param name="provider">The format provider to use.</param>
		/// <param name="styles">The styles to use.</param>
		/// <returns></returns>
		public static Optional<System.DateTime> DateTime(
			string value, string format = null, IFormatProvider provider = null, DateTimeStyles styles = DateTimeStyles.AssumeLocal) {
			var none = None(MissingReasons.CouldNotBeParsedAs<SystemDateTime>.Value);
			if (format == null) {
				return System.DateTime.TryParse(value, provider, styles, out var a) ? Some(a) : none;
			}
			return SystemDateTime.TryParseExact(value, format, provider, styles, out var b) ? Some(b) : none;
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
		/// Tries to parse a <see cref="System.Guid"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="format">The format of the Guid.</param>
		/// <returns></returns>
		public static Optional<Guid> Guid(string value, string format = null) {
			var none = None(MissingReasons.CouldNotBeParsedAs<Guid>.Value);
			if (format == null) {
				return System.Guid.TryParse(value, out var a) ? Some(a) : none; 
			}
			return System.Guid.TryParseExact(value, format, out var x) ? Some(x) : none;
		}

		/// <summary>
		/// Tries to parse a <see cref="System.TimeSpan"/>, returning None if the parsing fails.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="format">The format of the time span.</param>
		/// <param name="provider">The format provider.</param>
		/// <returns></returns>
		public static Optional<TimeSpan> TimeSpan(string value, string format = null, IFormatProvider provider = null) {
			var none = None(MissingReasons.CouldNotBeParsedAs<TimeSpan>.Value);
			if (format == null) {
				return System.TimeSpan.TryParse(value, provider, out var a) ? Some(a) : none;
			}
			return System.TimeSpan.TryParseExact(value, format, provider, out var b) ? Some(b) : none;
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
	
	}
}
