using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2022.Day07
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			Dictionary<string, int> tree = GetDirectoryTree(lines);

			var sum = tree.Where(x => x.Value <= 100000).Select(x => x.Value).Sum();

			Console.WriteLine($"Sum of directories that contain at most 100000: {sum}");
		}

		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			Dictionary<string, int> tree = GetDirectoryTree(lines);

			var updateSize = 70000000;
			var runSize = 30000000;
			var availableSize = updateSize - tree[@"/"];


			var largestAvailable = tree.Where(x => x.Value > runSize - availableSize).Select(x => x.Value).Min();


			Console.WriteLine($"Smallest directory that can be deleted has the size: {largestAvailable}");

		}

		private int DirSize(string[] dirContent)
		{
			var result = 0;
			foreach (var line in dirContent)
			{
				if (!line.StartsWith("dir "))
				{
					result += int.Parse(line.Split(" ")[0]);
				}
			}

			return result;
		}

		private Dictionary<string, int> GetDirectoryTree(string[] lines)
		{
			var tree = new Dictionary<string, int>();

			string currentDir = string.Empty;
			int index = 0;

			while (index < lines.Length)
			{
				var line = lines[index];

				if (line.StartsWith("$ cd"))
				{
					if (line == "$ cd ..")
					{
						var lastSlash = currentDir.LastIndexOf("/");
						currentDir = currentDir.Substring(0, lastSlash);
					}
					else if (line == "$ cd /")
					{
						currentDir = @"/";
						tree.Add(currentDir, 0);
					}
					else
					{
						currentDir += @"/" + line.Replace("$ cd ", "");
						tree.Add(currentDir, 0);
					}
					index++;
					continue;
				}
				else if (line.StartsWith("$ ls"))
				{
					index++;
					continue;
				}
				else
				{
					List<string> content = new();

					while (index < lines.Length && !lines[index].StartsWith("$"))
					{
						content.Add(lines[index]);
						index++;
					}

					int dirSize = DirSize(content.ToArray());

					foreach (var item in tree)
					{
						if (currentDir.StartsWith(item.Key))
						{
							int value;
							tree.TryGetValue(item.Key, out value);
							tree[item.Key] = value + dirSize;
						}
					}
				}
			}

			return tree;
		}
	}
}
