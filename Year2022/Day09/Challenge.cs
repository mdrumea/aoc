using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.HighPerformance;

namespace AdventOfCode.Year2022.Day09
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			var head = (Row: 0, Col: 0);
			var tail = (Row: 0, Col: 0);
			List<(int, int)> positions = new List<(int, int)>() { tail };

			foreach (var line in lines)
			{
				var parts = line.Split(" ");

				var stepRow = 0;
				var stepCol = 0;

				switch (parts[0])
				{
					case "D":
						stepRow = 1;
						break;
					case "R":
						stepCol = 1;
						break;
					case "U":
						stepRow = -1;
						break;
					case "L":
						stepCol = -1;
						break;

					default:
						throw new InvalidOperationException();
				}

				for (int i = 0; i < int.Parse(parts[1]); i++)
				{
					head = (head.Row + stepRow, head.Col + stepCol);

					var dX = head.Row - tail.Row;
					var dY = head.Col - tail.Col;

					if (Math.Abs(dX) > 1 || Math.Abs(dY) > 1)
					{
						tail.Row += Math.Sign(dX);
						tail.Col += Math.Sign(dY);
					}

					if (!positions.Contains(tail))
					{
						positions.Add(tail);
					}
				}
			}

			Console.WriteLine($"Unique locations of the tail: {positions.Count}");
		}

		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			var tail = (Row: 0, Col: 0);

			(int Row, int Col)[] rope = Enumerable.Range(0, 10).Select(x => (0, 0)).ToArray(); ;

			List<(int, int)> positions = new List<(int, int)>() { tail };

			foreach (var line in lines)
			{
				var parts = line.Split(" ");

				var stepRow = 0;
				var stepCol = 0;

				for (int i = 0; i < int.Parse(parts[1]); i++)
				{
					switch (parts[0])
					{
						case "D":
							stepRow = 1;
							break;
						case "R":
							stepCol = 1;
							break;
						case "U":
							stepRow = -1;
							break;
						case "L":
							stepCol = -1;
							break;

						default:
							throw new InvalidOperationException();
					}
					rope[0] = (rope[0].Row + stepRow, rope[0].Col + stepCol);

					for (int j = 1; j < rope.Length; j++)
					{
						var dX = rope[j - 1].Row - rope[j].Row;
						var dY = rope[j - 1].Col - rope[j].Col;

						if (Math.Abs(dX) > 1 || Math.Abs(dY) > 1)
						{
							rope[j].Row += Math.Sign(dX);
							rope[j].Col += Math.Sign(dY);
						}
					}

					if (!positions.Contains(rope[^1]))
					{
						positions.Add(rope[^1]);
					}
				}
			}

			Console.WriteLine($"Unique locations of the tail: {positions.Count}");

		}
	}
}
