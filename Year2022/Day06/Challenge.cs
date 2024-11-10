using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2022.Day06
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			var line = lines[0];

			int count = 0;
			for (int i = 4; i < line.Length; i++)
			{
				if (line.Substring(i-4, 4).ToHashSet().Count() == 4)
				{
					count = i;
					break;
				}
			}

			Console.WriteLine($"Characters processed until start of message: {count}");
		}

		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			var line = lines[0];

			int count = 0;
			for (int i = 14; i < line.Length; i++)
			{
				if (line.Substring(i - 14, 14).ToHashSet().Count() == 14)
				{
					count = i;
					break;
				}
			}

			Console.WriteLine($"Characters processed until start of message: {count}");
		}
	}
}
