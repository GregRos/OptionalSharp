﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionalSharp.Tests
{
	class Program
	{
		static void Main(string[] args) {
			var x = Optional.None("Test").Cast<int>();
			var v = Optional.Some(5);
		}
	}
}
