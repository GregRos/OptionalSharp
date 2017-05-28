using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OptionalSharp.Optional;
namespace OptionalSharp
{
	class Program
	{
		static void Main(string[] args) {
			var y = Some(5);
			var x = Some(Tuple.Create(1, 2));

			var t = Some(Task.FromResult(5));
		}
	}
}
