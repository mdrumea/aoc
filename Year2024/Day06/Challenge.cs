using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.HighPerformance;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode.Year2024.Day06
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			string[,] grid = new string[lines.Length, lines[0].Length];

			List<(int, int)> gridPoints = GetVisitedPoints(lines);

			Console.WriteLine($"Positions visited at least once: {gridPoints.Count}");

		}

		private string GetDirection(string dir) => dir switch
		{
			"^" => ">",
			">" => "V",
			"V" => "<",
			"<" => "^",
			_ => ""
		};

		private List<(int, int)> GetVisitedPoints(string[] lines)
		{
			(int, int) current = (0, 0);
			
			string dir = "";

			var grid = LoadGrid(lines, ref current, ref dir);

			List<(int, int)> gridPoints = new List<(int, int)>() { current };

			while (current.Item1 > 0 || current.Item1 <= grid.RowCount() || current.Item2 > 0 || current.Item2 <= grid.ColumnCount())
			{
				if (dir == "^")
				{
					if (current.Item1 - 1 < 0) break;
					if (grid[current.Item1 - 1, current.Item2] != "#")
					{
						current = (current.Item1 - 1, current.Item2);
						if (!gridPoints.Contains(current)) gridPoints.Add(current);
					}
					else
					{
						dir = GetDirection(dir);
						continue;
					}
				}
				if (dir == ">")
				{
					if (current.Item2 + 1 > grid.ColumnCount() - 1) break;
					if (grid[current.Item1, current.Item2 + 1] != "#")
					{
						current = (current.Item1, current.Item2 + 1);
						if (!gridPoints.Contains(current)) gridPoints.Add(current);
					}
					else
					{
						dir = GetDirection(dir);
						continue;
					}
				}
				if (dir == "V")
				{
					if (current.Item1 + 1 > grid.RowCount() - 1) break;
					if (grid[current.Item1 + 1, current.Item2] != "#")
					{
						current = (current.Item1 + 1, current.Item2);
						if (!gridPoints.Contains(current)) gridPoints.Add(current);
					}
					else
					{
						dir = GetDirection(dir);
						continue;
					}
				}
				if (dir == "<")
				{
					if (current.Item2 - 1 < 0) break;
					if (grid[current.Item1, current.Item2 - 1] != "#")
					{
						current = (current.Item1, current.Item2 - 1);
						if (!gridPoints.Contains(current)) gridPoints.Add(current);
					}
					else
					{
						dir = GetDirection(dir);
						continue;
					}
				}


			}

			return gridPoints;
		}

		private static string[,] LoadGrid(string[] lines, ref (int, int) start, ref string dir)
		{
			string[,] grid = new string[lines.Length, lines[0].Length]; 

			for (int i = 0; i < grid.RowCount(); i++)
			{
				for (int j = 0; j < grid.ColumnCount(); j++)
				{
					grid[i, j] = lines[i][j].ToString();
					if ((new HashSet<string> { "^", ">", "<", "V" }).Contains(grid[i, j]))
					{
						start = (i, j);

						dir = grid[i, j];

					}
				}
			}

			return grid;
		}

		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			string[,] grid = new string[lines.Length, lines[0].Length];

			(int, int) current = (0, 0);

			string dir = "";

			grid = LoadGrid(lines, ref current, ref dir);

			List<(int, int)> visitedPoints = GetVisitedPoints(lines);

			var count = 0;

			for (int i=0; i < visitedPoints.Count; i++)
			{
				(int, int) start;

				grid = LoadGrid(lines, ref current, ref dir);

				if (visitedPoints[i] == current)
				{
					continue;
				}
				else
				{
					grid[visitedPoints[i].Item1, visitedPoints[i].Item2] = "#";

					if (IsInfiniteLoop(grid, current, dir, visitedPoints[i]))
					{
						count++;
						continue;
					}
									
				}
			}

			Console.WriteLine($"Infinite loops obtained by adding one blocker: {count}");
		}

		public bool IsInfiniteLoop(string[,] grid, (int, int) start, string dir, (int, int) blocker)
		{
			grid[blocker.Item1, blocker.Item2] = "#";

			(int, int) current = start;

			List<((int, int), string)> gridPoints = new List<((int, int), string)>(); // store visited points AND direction. If the same point is visited in the same direction it's an infinite loop


			while (current.Item1 > 0 || current.Item1 <= grid.RowCount() || current.Item2 > 0 || current.Item2 <= grid.ColumnCount())
			{
				if (dir == "^")
				{
					if (current.Item1 - 1 < 0) break;
					if (grid[current.Item1 - 1, current.Item2] != "#")
					{
						current = (current.Item1 - 1, current.Item2);
						if (!gridPoints.Contains((current, dir)))
							gridPoints.Add((current, dir));
						else
						{
							return true;
						}
					}
					else
					{
						dir = GetDirection(dir);
						continue;
					}
				}
				if (dir == ">")
				{
					if (current.Item2 + 1 > grid.ColumnCount() - 1) break;
					if (grid[current.Item1, current.Item2 + 1] != "#")
					{
						current = (current.Item1, current.Item2 + 1);
						if (!gridPoints.Contains((current, dir)))
							gridPoints.Add((current, dir));
						else
						{
							return true;
						}
					}
					else
					{
						dir = GetDirection(dir);
						continue;
					}
				}
				if (dir == "V")
				{
					if (current.Item1 + 1 > grid.RowCount() - 1) break;
					if (grid[current.Item1 + 1, current.Item2] != "#")
					{
						current = (current.Item1 + 1, current.Item2);
						if (!gridPoints.Contains((current, dir)))
							gridPoints.Add((current, dir));
						else
						{
							return true;
						}
					}
					else
					{
						dir = GetDirection(dir);
						continue;
					}
				}
				if (dir == "<")
				{
					if (current.Item2 - 1 < 0) break;
					if (grid[current.Item1, current.Item2 - 1] != "#")
					{
						current = (current.Item1, current.Item2 - 1);
						if (!gridPoints.Contains((current, dir)))
							gridPoints.Add((current, dir));
						else
						{
							return true;
						}
					}
					else
					{
						dir = GetDirection(dir);
						continue;
					}
				}
			}

			return false;
		}

	}
}
