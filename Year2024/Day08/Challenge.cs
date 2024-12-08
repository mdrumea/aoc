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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Year2024.Day08
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			string[,] grid = new string[lines.Length, lines[0].Length];

			Dictionary<string, List<(int, int)>> map = new Dictionary<string, List<(int, int)>>();

			for (int i = 0; i < grid.RowCount(); i++)
			{
				for (int j = 0; j < grid.ColumnCount(); j++)
				{
					grid[i, j] = lines[i][j].ToString();
					if (grid[i, j] != ".")
					{
						if (map.ContainsKey(grid[i, j]))
						{
							map[grid[i, j]].Add((i, j));
						}
						else
						{
							map.Add(grid[i, j], new List<(int, int)>() { (i, j) });
						}
					}
				}
			}

			var antList = new List<(int, int)>();

			foreach (var pair in map)
			{
				for (int i = 0; i < pair.Value.Count - 1; i++)
				{
					for (int j = i + 1; j < pair.Value.Count; j++)
					{
						var diffRow = pair.Value[j].Item1 - pair.Value[i].Item1;
						var diffCol = pair.Value[j].Item2 - pair.Value[i].Item2;


						var rowN1 = pair.Value[i].Item1 - diffRow;
						var colN1 = pair.Value[i].Item2 - diffCol;

						var rowN2 = pair.Value[j].Item1 + diffRow;
						var colN2 = pair.Value[j].Item2 + diffCol;

						if (0 <= rowN1 && rowN1 < grid.RowCount() && 0 <= colN1 && colN1 < grid.ColumnCount())
						{
							if (!antList.Contains((rowN1, colN1)))
								antList.Add((rowN1, colN1));
						}

						if (0 <= rowN2  && rowN2 < grid.RowCount() && 0 <= colN2 && colN2 < grid.ColumnCount())
						{
							if (!antList.Contains((rowN2, colN2)))
								antList.Add((rowN2, colN2));
						}

					}
				}
			}

			Console.WriteLine($"Unique locations of antinodes: {antList.Count}");

		}


		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			string[,] grid = new string[lines.Length, lines[0].Length];

			Dictionary<string, List<(int, int)>> map = new Dictionary<string, List<(int, int)>>();

			for (int i = 0; i < grid.RowCount(); i++)
			{
				for (int j = 0; j < grid.ColumnCount(); j++)
				{
					grid[i, j] = lines[i][j].ToString();
					if (grid[i, j] != ".")
					{
						if (map.ContainsKey(grid[i, j]))
						{
							map[grid[i, j]].Add((i, j));
						}
						else
						{
							map.Add(grid[i, j], new List<(int, int)>() { (i, j) });
						}
					}
				}
			}

			var nodeList = new List<(int, int)>();

			foreach (var pair in map)
			{
				for (int i = 0; i < pair.Value.Count - 1; i++)
				{
					for (int j = i + 1; j < pair.Value.Count; j++)
					{
						var diffRow = pair.Value[j].Item1 - pair.Value[i].Item1;
						var diffCol = pair.Value[j].Item2 - pair.Value[i].Item2;

						var rowN1 = pair.Value[i].Item1;
						var colN1 = pair.Value[i].Item2;

						var rowN2 = pair.Value[j].Item1;
						var colN2 = pair.Value[j].Item2;

						while (0 <= rowN1 && rowN1 < grid.RowCount() && 0 <= colN1 && colN1 < grid.ColumnCount())
						{
							if (!nodeList.Contains((rowN1, colN1)))
								nodeList.Add((rowN1, colN1));

							rowN1 -= diffRow;
							colN1 -= diffCol;
						}

						while (0 <= rowN2 && rowN2 < grid.RowCount() && 0 <= colN2 && colN2 < grid.ColumnCount())
						{

							if (!nodeList.Contains((rowN2, colN2)))
								nodeList.Add((rowN2, colN2));

							rowN2 += diffRow;
							colN2 += diffCol;
						}
					}
				}
			}

			Console.WriteLine($"Unique locations of new antinodes: {nodeList.Count}");

		}

	}
}
