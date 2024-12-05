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

namespace AdventOfCode.Year2024.Day05
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			var sum = 0;

			var idx = Array.IndexOf(lines, "");

			var instr = lines.Take(idx).ToArray();
			var pages = lines.Skip(idx + 1).ToArray();


			foreach (var page in pages)
			{
				var parts = page.Split(",").Select(x => x).ToArray();

				var bad = false;
				for (int i = 0; i < parts.Length - 1; i++)
				{
					for (int j = i + 1; j < parts.Length; j++)
					{
						if (Array.IndexOf(instr, parts[i] + "|" + parts[j]) == -1)
						{
							bad = true;
							break;
						}
					}
					if (bad) continue;
				}

				if (!bad)
				{
					sum += int.Parse(parts.ElementAt(parts.Length / 2));
				}
			}

			Console.WriteLine($"Sum of middle page numbers of correct updates: {sum}");
		}


		public void Part2(Source source)
		{

			var lines = LoadSource(source);

			var sum = 0;

			var idx = Array.IndexOf(lines, "");

			var instr = lines.Take(idx).ToArray();
			var pages = lines.Skip(idx + 1).ToArray();

			List<string> incorr= new List<string>();

			foreach (var page in pages)
			{
				var parts = page.Split(",").Select(x => x).ToArray();

				var bad = false;
				for (int i = 0; i < parts.Length - 1; i++)
				{
					for (int j = i + 1; j < parts.Length; j++)
					{
						if (Array.IndexOf(instr, parts[i] + "|" + parts[j]) == -1)
						{
							bad = true;
							break;
						}
					}
					if (bad) 
					{
						incorr.Add(page);
						break;
					}
				}

			}

			Dictionary<string, HashSet<string>> map = new Dictionary<string, HashSet<string>>();

			foreach (var ins in instr)
			{
				var val = ins.Split("|").ToArray();

				if (map.ContainsKey(val.First())) 
				{
					map[val.First()].Add(val.Last());
				}
				else
				{
					map.Add(val.First(), new HashSet<string> { val.Last() });
				}

				if (!map.ContainsKey(val.Last()))
				{
					map.Add(val.Last(), new HashSet<string> ());
				}
			}


			foreach (var page in incorr)
			{
				var parts = page.Split(",").Select(x => x).ToArray();

				var temp = new Dictionary<string, int>();

				foreach (var part in parts)
				{
					temp.Add(part, parts.Where(x=>x!=part).Count(x => map[part].Contains(x)));
				}

				sum += int.Parse(temp.OrderBy(x => x.Value).Select(x => x.Key).ToArray().ElementAt(parts.Length / 2));
			}

			Console.WriteLine($"Sum of middle page numbers of incorrect updates: {sum}");
		}
	}
}
