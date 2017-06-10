using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptionalSharp.More;
using static OptionalSharp.Optional;
using Xunit;

namespace OptionalSharp.Tests.OptionalSharp.More {
	public static class ParsingTests {

		public static class Int16 {
			[Fact]
			static void Success() {
				Assert.Equal(TryParse.Int16("1"), Some((short) 1));
			}

			[Fact]
			static void Fail() {
				Assert.Equal(TryParse.Int16("a"), None());
			}

			[Fact]
			static void Null() {
				Assert.Equal(TryParse.Int16(null), None());
			}
		}

		public static class Int32 {
			[Fact]
			static void Success() {
				Assert.Equal(TryParse.Int32("1"), Some(1));
			}

			[Fact]
			static void Fail() {
				Assert.Equal(TryParse.Int32("a"), None());
			}

			[Fact]
			static void Null() {
				Assert.Equal(TryParse.Int32(null), None());
			}
		}



		public static class Int64 {
			[Fact]
			static void Success() {
				Assert.Equal(TryParse.Int64("1"), Some((long) 1));
			}

			[Fact]
			static void Fail() {
				Assert.Equal(TryParse.Int64("a"), None());
			}

			[Fact]
			static void Null() {
				Assert.Equal(TryParse.Int64(null), None());
			}
		}

		public static class UInt16 {
			[Fact]
			static void Success() {
				Assert.Equal(TryParse.UInt16("1"), Some((ushort) 1));
			}

			[Fact]
			static void Fail() {
				Assert.Equal(TryParse.UInt16("a"), None());
			}

			[Fact]
			static void Null() {
				Assert.Equal(TryParse.UInt16(null), None());
			}
		}

		public static class UInt32 {
			[Fact]
			static void Success() {
				Assert.Equal(TryParse.UInt32("1"), Some((uint) 1));
			}

			[Fact]
			static void Fail() {
				Assert.Equal(TryParse.UInt32("a"), None());
			}

			[Fact]
			static void Null() {
				Assert.Equal(TryParse.UInt32(null), None());
			}
		}

		public static class UInt64 {
			[Fact]
			static void Success() {
				Assert.Equal(TryParse.UInt64("1"), Some((ulong) 1));
			}

			[Fact]
			static void Fail() {
				Assert.Equal(TryParse.UInt64("a"), None());
			}

			[Fact]
			static void Null() {
				Assert.Equal(TryParse.UInt64(null), None());
			}
		}

		public static class Byte {
			[Fact]
			static void Success() {
				Assert.Equal(TryParse.Byte("1"), Some((byte) 1));
			}

			[Fact]
			static void Fail() {
				Assert.Equal(TryParse.Byte("a"), None());
			}

			[Fact]
			static void Null() {
				Assert.Equal(TryParse.Byte(null), None());
			}
		}

		public static class SByte {
			[Fact]
			static void Success() {
				Assert.Equal(TryParse.SByte("1"), Some((sbyte) 1));
			}

			[Fact]
			static void Fail() {
				Assert.Equal(TryParse.SByte("a"), None());
			}

			[Fact]
			static void Null() {
				Assert.Equal(TryParse.SByte(null), None());
			}
		}

		public static class Float {
			[Fact]
			static void Success() {
				Assert.Equal(TryParse.Float("1.0"), Some((float) 1));
			}

			[Fact]
			static void Fail() {
				Assert.Equal(TryParse.Float("a"), None());
			}

			[Fact]
			static void Null() {
				Assert.Equal(TryParse.Float(null), None());
			}
		}

		public static class Double {
			[Fact]
			static void Success() {
				Assert.Equal(TryParse.Double("1"), Some((double) 1));
			}

			[Fact]
			static void Fail() {
				Assert.Equal(TryParse.Double("a"), None());
			}

			[Fact]
			static void Null() {
				Assert.Equal(TryParse.Double(null), None());
			}
		}

		public static class Bool {
			[Fact]
			static void Success() {
				Assert.Equal(TryParse.Bool("true"), Some(true));
			}

			[Fact]
			static void Fail() {
				Assert.Equal(TryParse.Bool("a"), None());
			}

			[Fact]
			static void Null() {
				Assert.Equal(TryParse.Bool(null), None());
			}
		}

		public static class Decimal {
			[Fact]
			static void Success() {
				Assert.Equal(TryParse.Decimal("1"), Some((decimal) 1));
			}

			[Fact]
			static void Fail() {
				Assert.Equal(TryParse.Decimal("a"), None());
			}

			[Fact]
			static void Null() {
				Assert.Equal(TryParse.Decimal(null), None());
			}
		}

		public static class Char {
			[Fact]
			static void Success() {
				Assert.Equal(TryParse.Char("1"), Some('1'));
			}

			[Fact]
			static void Fail() {
				Assert.Equal(TryParse.Char("ab"), None());
			}

			[Fact]
			static void Null() {
				Assert.Equal(TryParse.Char(null), None());
			}
		}
	}
}
