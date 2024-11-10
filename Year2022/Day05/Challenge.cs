using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2022.Day05
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);


			var current = 0;

			while (lines[current] != "")
			{
				var line = lines[current];
				current++;
			}

			var stacks = lines[current - 1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => new Stack<string>()).ToArray();

			for (int i = current - 2; i >=0; i--)
			{
				for (int j = 1; j <= stacks.Length; j++)
				{
					if (lines[i].Substring(4 * j - 3, 1) != " ")
					{
						stacks[j-1].Push(lines[i].Substring(4 * j - 3, 1));
					}
				}
			}


			current++; // get to instructions
			while (current < lines.Length)
			{
				var line = lines[current];

				string pattern = @"\d+";
				var inst = Regex.Matches(line, pattern).Select(x => int.Parse(x.Value)).ToArray();

				for (int k = 1; k <= inst[0]; k++)
				{
					var crate = stacks[inst[1]-1].Pop();
					stacks[inst[2]-1].Push(crate);
				}

				current++;
			}


			StringBuilder topCrates = new();

			for (int i = 0; i < stacks.Length; i++)
			{
				topCrates.Append(stacks[i].Peek());
			}


			Console.WriteLine($"Top crates: {topCrates.ToString()}");
		}

		public void Part2(Source source)
		{
			var lines = LoadSource(source);


			var current = 0;

			while (lines[current] != "")
			{
				var line = lines[current];
				current++;
			}

			var stacks = lines[current - 1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => new Stack<string>()).ToArray();

			for (int i = current - 2; i >= 0; i--)
			{
				for (int j = 1; j <= stacks.Length; j++)
				{
					if (lines[i].Substring(4 * j - 3, 1) != " ")
					{
						stacks[j - 1].Push(lines[i].Substring(4 * j - 3, 1));
					}
				}
			}


			current++; // get to instructions
			while (current < lines.Length)
			{
				var line = lines[current];

				string pattern = @"\d+";
				var inst = Regex.Matches(line, pattern).Select(x => int.Parse(x.Value)).ToArray();


				var tempStack = new Stack<string>();

				for (int k = 1; k <= inst[0]; k++)
				{
					var crate = stacks[inst[1] - 1].Pop();
					tempStack.Push(crate);
				}

				for (int k = 1; k <= inst[0]; k++)
				{
					var crate = tempStack.Pop();
					stacks[inst[2] - 1].Push(crate);
				}


				current++;
			}


			StringBuilder topCrates = new();

			for (int i = 0; i < stacks.Length; i++)
			{
				topCrates.Append(stacks[i].Peek());
			}


			Console.WriteLine($"Top crates: {topCrates.ToString()}");
		}
	}
}
