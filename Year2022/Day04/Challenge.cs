using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2022.Day04
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			var count = 0;

			foreach (var line in lines) {
				var pairs = line.Split(',');

				var f = pairs[0].Split('-').Select(x => int.Parse(x)).ToArray();
				var s = pairs[1].Split('-').Select(x => int.Parse(x)).ToArray();

				if ((f[0] <= s[0] && f[1] >= s[1]) || // first includes second
					(s[0] <= f[0] && s[1] >= f[1]))   // second includes first
				{
					count++;
				}
			}

			Console.WriteLine($"Assignment pairs with one included in the other: {count}");
		}

		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			var count = 0;

			foreach (var line in lines)
			{
				var pairs = line.Split(',');

				var f = pairs[0].Split('-').Select(x => int.Parse(x)).ToArray();
				var s = pairs[1].Split('-').Select(x => int.Parse(x)).ToArray();

				if ((f[0] >= s[0] && f[0] <= s[1]) ||   // left side of first is in second
					(f[1] >= f[0] && f[1] <= s[1]) ||   // right side of first is in second
					(f[0] <= s[0] && f[1] >= s[1]) ||   // first includes second
					(s[0] <= f[0] && s[1] >= f[1]))     // second includes first
				{
					count++;
				}
			}

			Console.WriteLine($"Assignment pairs that partially overlap: {count}");
		}
	}
}
