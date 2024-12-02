using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.HighPerformance;

namespace AdventOfCode.Year2024.Day02
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		private static readonly HashSet<int> Constants = new HashSet<int> { 1, 2, 3 };

		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			var totalReports = 0;

			foreach (var line in lines)
			{
				var arr = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

				if (IsValid(arr))
				{
					totalReports++;
					continue;
				}
			}

			Console.WriteLine($"Total number of valid reports: {totalReports}");
		}

		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			var totalReports = 0;

			foreach (var line in lines)
			{
				var arr = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

				if (IsValid(arr))
				{
					totalReports++;
					continue;
				}

				for (int i = 0; i <= arr.Length - 1; i++)
				{
					var temp = arr.ToList();
					temp.RemoveAt(i);
					if (IsValid(temp.ToArray()))
					{
						totalReports++;
						break;
					}
				}

			}

			Console.WriteLine($"Total number of valid reports: {totalReports}");
		}

		public bool IsValid(int[] arr)
		{
			var differences = arr.Take(arr.Length - 1).Select((v, i) => arr[i + 1] - v).ToList();

			var mono = differences.All(x => Math.Abs(x) >= 1 && Math.Abs(x) <= 3);
			var sign = differences.All(x => x > 0) || differences.All(x => x < 0);

			return mono && sign;
		}

	}
}
