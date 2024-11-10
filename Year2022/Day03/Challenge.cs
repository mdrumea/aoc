using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2022.Day03
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			int sumPriorities = 0;

			foreach (var line in lines)
			{
				var st1 = line.Substring(0, line.Length / 2);
				var st2 = line.Substring(line.Length / 2, line.Length / 2);

				var set = st2.ToHashSet();

				for (int i = 0; i < st1.Length; i++) 
				{
					if (set.Contains(st1[i]))
					{
						sumPriorities += st1[i] switch
						{
							>= 'a' and <= 'z' => (int)st1[i] - 96,
							>= 'A' and <= 'Z' => (int)st1[i] - 38,
							_ => throw new NotImplementedException()
						};
						break;
					}
				}
			}


			Console.WriteLine($"Total sum of priorities: {sumPriorities}");
		}

		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			int sumPriorities = 0;

			var totalGroups = lines.Length / 3;

			for (int i = 0; i < totalGroups; i++)
			{
				var ch = '.';

				var set1 = lines[3*i].ToHashSet();
				var set2 = lines[3*i+1].ToHashSet();
				var set3 = lines[3*i+2].ToHashSet();

				for (int j = 1; j <= 52; j++)
				{
					ch = j switch
					{
						>= 1 and <= 26 => (char)(j + 96),
						>= 27 and <= 52 => (char)(j + 38),
						_ => throw new NotImplementedException()
					};

					if (set1.Contains(ch) && set2.Contains(ch) && set3.Contains(ch))
					{
						sumPriorities += ch switch
						{
							>= 'a' and <= 'z' => ch - 96,
							>= 'A' and <= 'Z' => ch - 38,
							_ => throw new NotImplementedException()
						};
						break;
					}
				}

			}

			Console.WriteLine($"Total sum of priorities of badges: {sumPriorities}");
		}
	}
}
