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

namespace AdventOfCode.Year2024.Day04
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);
			var wholeText = string.Join("", lines);

			var count = 0;

			string[,] grid = new string[lines.Length, lines[0].Length];

			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[0].Length; j++)
				{
					grid[i, j] = lines[i][j].ToString();
				}
			}

			for(int i = 0; i < lines[0].Length; i++)
			{
				for (int j = 0; j < lines[0].Length; j++)
				{
					if (j + 3 <= lines[0].Length - 1 && grid[i, j] + grid[i, j + 1] + grid[i, j + 2] + grid[i, j + 3] == "XMAS") count++;
					if (j - 3 >= 0 && grid[i, j] + grid[i, j - 1] + grid[i, j - 2] + grid[i, j - 3] == "XMAS") count++;
					if (i + 3 <= lines.Length - 1 && grid[i, j] + grid[i+1, j] + grid[i+2, j] + grid[i+3, j] == "XMAS") count++;
					if (i - 3 >= 0 && grid[i, j] + grid[i-1, j] + grid[i-2, j] + grid[i-3, j] == "XMAS") count++;
					if (j + 3 <= lines[0].Length - 1 && i - 3 >= 0 && grid[i, j] + grid[i - 1, j+1] + grid[i - 2, j+2] + grid[i - 3, j+3] == "XMAS") count++;
					if (j + 3 <= lines[0].Length - 1 && i + 3 <= lines.Length - 1 && grid[i, j] + grid[i + 1, j + 1] + grid[i + 2, j + 2] + grid[i + 3, j + 3] == "XMAS") count++;
					if (j - 3 >= 0 && i - 3 >= 0 && grid[i, j] + grid[i - 1, j - 1] + grid[i - 2, j - 2] + grid[i - 3, j - 3] == "XMAS") count++;
					if (j - 3 >= 0 && i + 3 <= lines.Length - 1 && grid[i, j] + grid[i + 1, j - 1] + grid[i + 2, j - 2] + grid[i + 3, j - 3] == "XMAS") count++;
				}
			}

			Console.WriteLine($"Count of XMAS: {count}");
		}


		public void Part2(Source source)
		{
			var lines = LoadSource(source);
			var wholeText = string.Join("", lines);

			var count = 0;

			string[,] grid = new string[lines.Length, lines[0].Length];

			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[0].Length; j++)
				{
					grid[i, j] = lines[i][j].ToString();
				}
			}

			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[0].Length; j++)
				{
					if (j - 1 >= 0 && j + 1 <= lines[0].Length - 1 && i - 1 >= 0 && i + 1 <= lines.Length - 1 &&
						(new HashSet<string>() { "MAS", "SAM" }).Contains(grid[i - 1, j - 1] + grid[i, j] + grid[i + 1, j + 1]) &&
						(new HashSet<string>() { "MAS", "SAM" }).Contains(grid[i - 1, j + 1] + grid[i, j] + grid[i + 1, j - 1]))
						count++;

			}
			}

			Console.WriteLine($"Count of MAS: {count}");

		}
	}
}
