using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.HighPerformance;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode.Year2024.Day03
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);
			var wholeText = string.Join("", lines);

			var sum = 0;

			sum += GetSum(wholeText);

			Console.WriteLine($"Multiplication result of {sum}");
		}


		public void Part2(Source source)
		{
			var lines = LoadSource(source);
			var wholeText = string.Join("", lines);

			var groups = wholeText.Split("don't()");

			var sum = GetSum(groups[0]);

			foreach (var group in groups.Skip(1))
			{
				var idx = group.IndexOf("do()");
				if (idx > 0)
				{
					sum += GetSum(group.Substring(idx));
				}
			}

			Console.WriteLine($"Multiplication result of {sum}");

		}

		private static int GetSum(string line)
		{
			var regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)");
			int result = 0;

			var matches = regex.Matches(line);

			foreach (Match match in matches)
			{
				var numbers = match.Value.Replace("mul(", "").Replace(")", "").Split(',').Select(int.Parse).ToArray();

				result += numbers[0] * numbers[1];

			}

			return result;
		}

	}
}
