using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2022.Day01
{
    internal class Day01 : BaseDayChallenge, IDayChallenge
    {
        public void Part1(Source source)
        {
            var lines = LoadSource(source);

			List<int> calories = GetCalories(lines);

			var maxCalories = calories.Max();

			Console.WriteLine($"Maximum calories carried by an elf: {maxCalories}");
		}

        public void Part2(Source source)
		{
			var lines = LoadSource(source);

			List<int> calories = GetCalories(lines);

			var top3Calories = calories.OrderDescending().Take(3).Sum();

			Console.WriteLine($"Sum of top 3 maximum calories carried by elfs: {top3Calories}");
		}

		private static List<int> GetCalories(string[] lines)
		{
			int currentCalories = 0;
			List<int> calories = new();

			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i] == string.Empty)
				{
					calories.Add(currentCalories);
					currentCalories = 0;
				}
				else
				{
					currentCalories += int.Parse(lines[i]);
				}

				if (i == lines.Length - 1)
				{
					calories.Add(currentCalories);
				}
			}

			return calories;
		}
	}
}
