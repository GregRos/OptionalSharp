using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OptionalSharp.More;
using static OptionalSharp.Optional;
namespace OptionalSharp.Tests.OptionalSharp.More
{
	public static partial class EnumerableTests
	{
		public static class TryFirst {
			[Fact]
			static void Simple_Success() {
				var seq = new[] {
					1, 2, 3
				};
				var tryFirst = seq.TryFirst();
				Assert.Equal(tryFirst, Some(1));
			}
			[Fact]
			static void Simple_Failure() {
				var seq = new int[0];

				var tryFirst = seq.TryFirst();
				Assert.Equal(tryFirst, None());
				Assert.Equal(tryFirst.Reason, MissingReasons.CollectionWasEmpty);
			}
			[Fact]
			static void Predicate_Success() {
				var seq = new int[] {
					1, 2, 3, 4
				};
				Assert.Equal(seq.TryFirst(x => x % 2 == 0), Some(2));
			}
			[Fact]
			static void Predicate_Failure() {
				var seq = new[] {
					1, 2, 3, 4
				};
				var tryFirst = seq.TryFirst(x => x > 5, "a");
				Assert.Equal(tryFirst, None());
				Assert.Equal(tryFirst.Reason, "a");
			}
		}

		public static class TryLast {
			[Fact]
			static void Simple_Success()
			{
				var seq = new[] {
					1, 2, 3
				};
				var tryFirst = seq.TryLast();
				Assert.Equal(tryFirst, Some(3));
			}
			[Fact]
			static void Simple_Failure()
			{
				var seq = new int[0];

				var tryFirst = seq.TryLast();
				Assert.Equal(tryFirst, None());
				Assert.Equal(tryFirst.Reason, MissingReasons.CollectionWasEmpty);
			}
			[Fact]
			static void Predicate_Success()
			{
				var seq = new int[] {
					1, 2, 3, 4
				};
				Assert.Equal(seq.TryLast(x => x % 2 == 0), Some(4));
			}
			[Fact]
			static void Predicate_Failure()
			{
				var seq = new[] {
					1, 2, 3, 4
				};
				var tryFirst = seq.TryLast(x => x > 5, "a");
				Assert.Equal(tryFirst, None());
				Assert.Equal(tryFirst.Reason, "a");
			}
		}

		public static class TrySingle {
			[Fact]
			static void Simple_Success()
			{
				var seq = new[] {
					1
				};
				var tryFirst = seq.TrySingle();
				Assert.Equal(tryFirst, Some(1));
			}
			[Fact]
			static void Simple_Failure()
			{
				var seq = new int[0];

				var tryFirst = seq.TrySingle();
				Assert.Equal(tryFirst, None());
				Assert.Equal(tryFirst.Reason, MissingReasons.CollectionWasEmpty);
			}
			[Fact]
			static void Predicate_Success()
			{
				var seq = new int[] {
					1, 2
				};
				Assert.Equal(seq.TrySingle(x => x % 2 == 0), Some(2));
			}
			[Fact]
			static void Predicate_Failure()
			{
				var seq = new[] {
					1, 2, 3, 4
				};
				var tryFirst = seq.TrySingle(x => x > 5, "a");
				Assert.Equal(tryFirst, None());
				Assert.Equal(tryFirst.Reason, "a");
			}

			[Fact]
			static void Predicate_MoreThanOneFound() {
				var seq = new[] {
					2, 2
				};

				Assert.Throws<ArgumentException>(() => seq.TrySingle(x => x == 2));
			}

			[Fact]
			static void Simple_MoreThanOneFound()
			{
				var seq = new[] {
					2, 2
				};

				Assert.Throws<ArgumentException>(() => seq.TrySingle());
			}
		}

		public static class TryElementAt {
			[Fact]
			static void List_Failure() {
				var x = new List<int>().TryElementAt(0);
				Assert.Equal(x, None());
				Assert.Equal(x.Reason, MissingReasons.IndexNotFound);
			}
			[Fact]
			static void List_Sucess() {
				var x = new List<int>() {
					1,
					2,
					3
				}.TryElementAt(2);
				Assert.Equal(x, Some(3));
			}
			[Fact]
			static void Seq_Failure() {
				var a = new[] {
					1, 2, 3
				}.Select(x => x).TryElementAt(5);

				Assert.Equal(a, None());
				Assert.Equal(a.Reason, MissingReasons.IndexNotFound);
			}
			[Fact]
			static void Seq_Success()
			{
				var a = new[] {
					1, 2, 3
				}.Select(x => x).TryElementAt(1);

				Assert.Equal(a, Some(2));
			}
		}

		public static class TryGet {
			
			static Dictionary<int, int> dict = new Dictionary<int, int> {
				{
					1, 1
				}, {
					2, 2
				}
			};
			static IEnumerable<KeyValuePair<int, int>> seq = Enumerable.Range(0, 10).Select(x => new KeyValuePair<int, int>(x, x % 5));
			[Fact]
			static void Dictionary_Success() {
				Assert.Equal(dict.TryKey(2), Some(2));
			}
			[Fact]
			static void Dictionary_Failure() {
				var v = dict.TryKey(3);
				Assert.Equal(v, None());
				Assert.Equal(v.Reason, MissingReasons.KeyNotFound);
			}
			[Fact]
			static void Seq_Success() {
				Assert.Equal(seq.TryKey(2), Some(2));
			}
			[Fact]
			static void Seq_Failure() {
				var v = seq.TryKey(11);
				Assert.Equal(None(), v);
				Assert.Equal(v.Reason, MissingReasons.KeyNotFound);
			}
		}

		public static class Choose {
			[Fact]
			static void Seq_Choose_NonEmpty() {
				var x = new[] {
					1, 2, 3
				}.Choose(a => a == 2 ? Some(a) : None());
				Assert.Equal(x, new[] {
					2
				});
			}

			[Fact]
			static void Seq_Choose_Empty() {
				var x = new[]{ 1, 2, 3}.Choose(y => NoneOf<int>());

				Assert.Equal(x, new int[0]);
			}
		}

		public static class TryPick {
			[Fact]
			static void NotFound() {
				var a = new[] {
					1, 2, 3
				}.TryPick(x => x == 5 ? Some(x) : None("a"));

				Assert.Equal(a, None());
				Assert.Equal(a.Reason, "a");
			}

			[Fact]
			static void Found() {
				var a = new[] {
					1, 2, 3
				}.TryPick(x => x >= 2 ? Some(x) : None());

				Assert.Equal(a, Some(2));
			}

			[Fact]
			static void Empty() {
				var a = new int[0].TryPick(Some);

				Assert.Equal(a, None());
				Assert.Equal(a.Reason, MissingReasons.NoElementsFound);
			}
		}

		public static class SelectMany {
			[Fact]
			static void SomeResult() {
				var a = Some(5).SelectMany(x => Enumerable.Range(0, 5));
				Assert.Equal(a, new[] {
					0, 1, 2, 3, 4
				});
			}
			[Fact]
			static void None_Result() {
				var a = NoneOf<int>().SelectMany(x => Enumerable.Range(0, 5));
				Assert.Equal(a, new int[0]);
			}
			[Fact]
			static void Some_ToEmpty() {
				var a = Some(5).SelectMany(x => new int[0]);
				Assert.Equal(a, new int[0]);
			}
		}

		public static class Flatten {
			[Fact]
			static void SeqOfOptionals() {
				var a = Enumerable.Range(0, 10).Select(x => x < 5 ? Some(x) : None()).FlattenSequence();
				Assert.Equal(a, new[] {
					0, 1, 2, 3, 4
				});
			}

			[Fact]
			static void SeqOfOptionalsNone() {
				var a = Enumerable.Range(0, 10).Select(x => NoneOf<int>()).FlattenSequence();
				Assert.Equal(a, new int[0]);
			}
		}

	}
}
