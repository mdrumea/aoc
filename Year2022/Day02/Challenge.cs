using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2022.Day02
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			var scores = new List<int>();

			for (int i = 0; i < lines.Length; i++)
			{
				var bets = lines[i].Split(' ');

				int outcomeScore = bets[0] switch
				{
					"A" => bets[1] switch { "X" => 3, "Y" => 6, "Z" => 0, _ => throw new NotImplementedException() },
					"B" => bets[1] switch { "X" => 0, "Y" => 3, "Z" => 6, _ => throw new NotImplementedException() },
					"C" => bets[1] switch { "X" => 6, "Y" => 0, "Z" => 3, _ => throw new NotImplementedException() },
					_ => throw new NotImplementedException()
				};

				int shapeScore = bets[1] switch
				{
					"X" => 1,
					"Y" => 2,
					"Z" => 3,
					_ => throw new NotImplementedException()
				};

				scores.Add(outcomeScore + shapeScore);
			}

			Console.WriteLine($"Total score: {scores.Sum()}");
		}

		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			var scores = new List<int>();

			for (int i = 0; i < lines.Length; i++)
			{
				var bets = lines[i].Split(' ');

				string realShape = bets[0] switch
				{
					"A" => bets[1] switch { "X" => "Z", "Y" => "X", "Z" => "Y", _ => throw new NotImplementedException() },
					"B" => bets[1] switch { "X" => "X", "Y" => "Y", "Z" => "Z", _ => throw new NotImplementedException() },
					"C" => bets[1] switch { "X" => "Y", "Y" => "Z", "Z" => "X", _ => throw new NotImplementedException() },
					_ => throw new NotImplementedException()
				};
				
				int shapeScore = realShape switch
				{
					"X" => 1,
					"Y" => 2,
					"Z" => 3,
					_ => throw new NotImplementedException()
				};

				int outcomeScore = bets[1] switch { "X" => 0, "Y" => 3, "Z" => 6, _ => throw new NotImplementedException() };

				scores.Add(outcomeScore + shapeScore);
			}

			Console.WriteLine($"Total score with new strategy: {scores.Sum()}");
		}
	}
}
