using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
	internal enum Source
	{
		Sample,
		Input
	}

	interface IDayChallenge
	{
		void Part1(Source source);

		void Part2(Source source);
	}
}
