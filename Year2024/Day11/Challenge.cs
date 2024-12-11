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

namespace AdventOfCode.Year2024.Day11
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			var stoneList = lines[0].Split(" ").Select(long.Parse).ToList<long>();

			CountStones(1, 25, ref stoneList);

			Console.WriteLine($"Stones count: {stoneList.Count}");
		}


		void CountStones(int blinkCount, int maxBlinkCount, ref List<long> stoneList)
		{
			if (blinkCount > maxBlinkCount)	return;

			List<long> tempStoneList = [];

			foreach (var stone in stoneList)
			{
				if (stone == 0)
				{
					tempStoneList.Add(1);

				}
				else if (stone.ToString().Length % 2 == 0)
				{
					var stone1 = long.Parse(stone.ToString().Substring(0, stone.ToString().Length / 2));
					var stone2 = long.Parse(stone.ToString().Substring(stone.ToString().Length / 2));
					tempStoneList.Add(stone1);
					tempStoneList.Add(stone2);
				}
				else
				{
					tempStoneList.Add(stone*2024);
				}
			}

			stoneList = [.. tempStoneList];

			CountStones(blinkCount+1, maxBlinkCount, ref stoneList);
		}

		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			var stoneList = lines[0].Split(" ").Select(long.Parse).ToList<long>();

			Dictionary<(long, long), long> dict = [];	

			long count = stoneList.Sum(x => CountStonesWithCache(1, 75, x, ref dict));

			Console.WriteLine($"Stones count: {count}");
		}


		long CountStonesWithCache(int blinkCount, int maxBlinkCount, long stone, ref Dictionary<(long, long), long> dict)
		{
			if (blinkCount > maxBlinkCount) return 1;

			long result = 0;

			if (dict.ContainsKey((blinkCount, stone)))
			{
				return dict[(blinkCount, stone)];
			}
			else
			{
				if (stone == 0)
				{
					result = CountStonesWithCache(blinkCount + 1, maxBlinkCount, 1, ref dict);
				}
				else if (stone.ToString().Length % 2 == 0)
				{
					var stone1 = long.Parse(stone.ToString().Substring(0, stone.ToString().Length / 2));
					var stone2 = long.Parse(stone.ToString().Substring(stone.ToString().Length / 2));

					result = CountStonesWithCache(blinkCount + 1, maxBlinkCount, stone1, ref dict) +
							 CountStonesWithCache(blinkCount + 1, maxBlinkCount, stone2, ref dict);
				}
				else
				{
					result = CountStonesWithCache(blinkCount + 1, maxBlinkCount, stone * 2024, ref dict);
				}

				dict.Add((blinkCount, stone), result);

				return result;
			}
		}

	}
}
