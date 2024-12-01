using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.HighPerformance;

namespace AdventOfCode.Year2024.Day01
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			var list1 = new List<int>();
			var list2 = new List<int>();

			foreach (var line in lines)
			{
				var parts = line.Split("   ");
				list1.Add(int.Parse(parts[0]));
				list2.Add(int.Parse(parts[1]));
			}

			list1.Sort();
			list2.Sort();

			var totalDistance = 0;

			for (int i = 0; i < list1.Count; i++)
			{
				totalDistance += Math.Abs(list1[i] - list2[i]);
			}

			Console.WriteLine($"Total distance is {totalDistance}");

		}

		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			var list1 = new List<int>();
			var list2 = new List<int>();

			foreach (var line in lines)
			{
				var parts = line.Split("   ");
				list1.Add(int.Parse(parts[0]));
				list2.Add(int.Parse(parts[1]));
			}

			var similarityCode = 0;

			for (int i = 0; i < list1.Count; i++)
			{
				similarityCode += list2.Count(x => x == list1[i])* list1[i];
			}

			Console.WriteLine($"Similarity code is {similarityCode}");
		}
	}
}
