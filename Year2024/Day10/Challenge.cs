using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.HighPerformance;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Year2024.Day10
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);
			int[,] grid = new int[lines.Length, lines[0].Length];

			Dictionary<(int, int), List<List<(int, int)>>> map = [];

			for (int i = 0; i < grid.RowCount(); i++)
			{
				for (int j = 0; j < grid.ColumnCount(); j++)
				{
					grid[i, j] = int.Parse(lines[i][j].ToString());
					if (grid[i, j] == 0)
					{
						map.Add((i, j), []);
					}
				}
			}

			foreach (var key in map.Keys)
			{
				FindPaths(key.Item1, key.Item2, 0, grid, [], map, "part1");
			}

			var result = map.Values.SelectMany(x => x).Count();

			Console.WriteLine(result);


		}


		void FindPaths(int row, int col, int value, int[,] grid, List<(int, int)> path, Dictionary<(int, int), List<List<(int, int)>>> map, string part)
		{
			if (value > 9) return;

			path.Add((row, col));

			if (value == 9)
			{
				if (part == "part1")
				{
					if (!map[path[0]].Any(x => x[0] == path[0] && x[^1] == path[^1])) // stupid condition cost me 2 hours!!
					{
						map[path[0]].Add(new(path));
					}
					path.RemoveAt(path.Count - 1);
					return;
				}
				else
				{
					{
						map[path[0]].Add(new(path));
						path.RemoveAt(path.Count - 1);
						return;

					}
				}
			}

			if (row - 1 >= 0 && grid[row - 1, col] == value + 1)
			{
				FindPaths(row - 1, col, value + 1, grid, path, map, part);
			}
			
			if (row +1 < grid.RowCount() && grid[row + 1, col] == value + 1)
			{
				FindPaths(row + 1, col, value + 1, grid, path, map, part);
			}

			if (col - 1 >= 0 && grid[row, col - 1] == value + 1)
			{
				FindPaths(row, col - 1, value + 1, grid, path, map, part);
			}

			if (col + 1 < grid.ColumnCount() && grid[row, col + 1] == value + 1)
			{
				FindPaths(row, col + 1, value + 1, grid, path, map, part);
			}

			path.RemoveAt(path.Count - 1);
		}


		public void Part2(Source source)
		{
			var lines = LoadSource(source);
			int[,] grid = new int[lines.Length, lines[0].Length];

			Dictionary<(int, int), List<List<(int, int)>>> map = [];

			for (int i = 0; i < grid.RowCount(); i++)
			{
				for (int j = 0; j < grid.ColumnCount(); j++)
				{
					grid[i, j] = int.Parse(lines[i][j].ToString());
					if (grid[i, j] == 0)
					{
						map.Add((i, j), []);
					}
				}
			}

			foreach (var key in map.Keys)
			{
				FindPaths(key.Item1, key.Item2, 0, grid, [], map, "part2");
			}

			var result = map.Values.SelectMany(x => x).Count();

			Console.WriteLine(result);
		}
	}
}
