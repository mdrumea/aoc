using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.HighPerformance;

namespace AdventOfCode.Year2022.Day08
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			//int[,] grid = new int[lines.Length, lines[0].Length];

			int[,] grid = lines.ToMatrix<int>();


			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[0].Length; j++)
				{
					grid[i, j] = int.Parse(lines[i][j].ToString());
				}
			}


			int count = 2 * (grid.RowCount() + grid.ColumnCount() - 2); // exterior trees


			for (int i = 1; i < grid.RowCount() - 1; i++)
			{
				for (int j = 1; j < grid.ColumnCount() - 1; j++)
				{
					var isVisibleW = grid.GetRow(i).ToArray().Select((t, idx) => new { t, idx }).Where(pair => 0 <= pair.idx && pair.idx < j).All(pair => pair.t < grid[i, j]);
					var isVisibleE = grid.GetRow(i).ToArray().Select((t, idx) => new { t, idx }).Where(pair => j < pair.idx && pair.idx <= grid.ColumnCount() -1).All(pair => pair.t < grid[i, j]);
					var isVisibleN = grid.GetColumn(j).ToArray().Select((t, idx) => new { t, idx }).Where(pair => 0 <= pair.idx && pair.idx < i).All(pair => pair.t < grid[i, j]);
					var isVisibleS = grid.GetColumn(j).ToArray().Select((t, idx) => new { t, idx }).Where(pair => i < pair.idx && pair.idx <= grid.RowCount() - 1).All(pair => pair.t < grid[i, j]);

					if (isVisibleW || isVisibleE || isVisibleN || isVisibleS)
					{
						count++;
					}
				}
			}

			Console.WriteLine($"Number of visible trees: {count}");
		}

		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			int[,] grid = new int[lines.Length, lines[0].Length];

			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[0].Length; j++)
				{
					grid[i, j] = int.Parse(lines[i][j].ToString());
				}
			}


			int maxScenicScore = 0;


			for (int i = 0; i <= grid.RowCount() - 1; i++)
			{
				for (int j = 0; j <= grid.ColumnCount() - 1; j++)
				{
					var scoreW = 0;
					var treesW = grid.GetRow(i).ToArray();
					for (int k = j - 1; k >= 0; k--)
					{
						if (treesW[k] < treesW[j])
						{
							scoreW++;
						}
						else
						{
							scoreW++;
							break;
						}
					}

					var scoreE = 0;
					var treesE = grid.GetRow(i).ToArray();
					for (int k = j +1 ; k <= grid.ColumnCount() - 1; k++)
					{
						if (treesE[k] < treesE[j])
						{
							scoreE++;
						}
						else
						{
							scoreE++;
							break;
						}
					}


					var scoreN = 0;
					var treesN = grid.GetColumn(j).ToArray();
					for (int k = i - 1; k >= 0; k--)
					{
						if (treesN[k] < treesN[i])
						{
							scoreN++;
						}
						else
						{
							scoreN++;
							break;
						}
					}

					var scoreS = 0;
					var treesS = grid.GetColumn(j).ToArray();
					for (int k = i + 1; k <= grid.RowCount() - 1; k++)
					{
						if (treesS[k] < treesS[i])
						{
							scoreS++;
						}
						else
						{
							scoreS++;
							break;
						}
					}

					maxScenicScore = Math.Max(maxScenicScore, scoreS * scoreN * scoreE * scoreW);
				}
			}

			Console.WriteLine($"Maximum scenic score: {maxScenicScore}");
		}
	}
}
