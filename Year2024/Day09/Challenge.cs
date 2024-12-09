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

namespace AdventOfCode.Year2024.Day09
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			var text = lines[0];

			List<(int Id, string Content)> disk = new();

			for (int i = 0; i < text.Length; i++)
			{
				var length = int.Parse(text[i].ToString());

				if (i % 2 == 0)
				{
					for (int j = 0; j < length; j++)
					{
						disk.Add((i / 2, "x"));
					}
				}
				else
				{
					for (int j = 0; j < length; j++)
					{
						disk.Add((-1, "."));
					}
				}

			}

			var firstSpace = disk.First(p => p.Id == -1);
			var lastBlock = disk.Last(p => p.Id != -1);
			var idxf = disk.LastIndexOf(lastBlock);
			var idxs = disk.IndexOf(firstSpace);

			(disk[idxs], disk[idxf]) = (disk[idxf], disk[idxs]);
			
			do
			{
				firstSpace = disk.First(p => p.Id == -1);
				lastBlock = disk.Last(p => p.Id != -1);
				idxf = disk.LastIndexOf(lastBlock);
				idxs = disk.IndexOf(firstSpace);

				if (idxf > idxs)
				{
					(disk[idxs], disk[idxf]) = (disk[idxf], disk[idxs]);
				}
			}
			while (idxf > idxs);

			long sum = 0;

			for (int i = 0; i < disk.Count; i++)
			{
				if (disk[i].Id != -1)
				{
					sum += disk[i].Id * i;
				}
			}

			Console.WriteLine($"Filesystem checksum {sum}");
		}


		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			var text = lines[0];

			List<(int Id, string Content)> disk = new();

			for (int i = 0; i < text.Length; i++)
			{
				var length = int.Parse(text[i].ToString());

				if (i % 2 == 0)
				{
					for (int j = 0; j < length; j++)
					{
						disk.Add((i / 2, "x"));
					}
				}
				else
				{
					for (int j = 0; j < length; j++)
					{
						disk.Add((-1, "."));
					}
				}

			}
			
			int lastFileEndIndex = disk.FindLastIndex(x => x.Id != -1);
			while (lastFileEndIndex >= 0)
			{
				if (disk.FindIndex(x => x.Id == -1) > lastFileEndIndex) break;

				if (disk[lastFileEndIndex].Id == -1) 
				{
					lastFileEndIndex--;
					continue;
				}

				int fileLength = 1;
				while (disk[lastFileEndIndex - fileLength].Id == disk[lastFileEndIndex].Id)
				{
					fileLength++;
				}

				bool start = false;
				int spaceLength = 0;
				int firstSpaceIndex = 0;
				for (int i = 0; i <= lastFileEndIndex - fileLength; i++)
				{
					if (disk[i].Id == -1)
					{
						if (!start)
						{
							start = true;
							firstSpaceIndex = i;

							spaceLength++;
						}
						else
						{
							spaceLength++;
						}
					}
					else
					{
						if (start)
						{
							if (spaceLength >= fileLength)
								break;
							else 
							{
								start = false;
								spaceLength = 0;
								continue;
							};
						}
						else
						{
							spaceLength = 0;
						}
					}
				}

				if (spaceLength >= fileLength)
				{
					for (int i = 0; i < fileLength; i++)
					{
						(disk[firstSpaceIndex + i], disk[lastFileEndIndex - i]) = (disk[lastFileEndIndex - i], disk[firstSpaceIndex + i]);
					}
					lastFileEndIndex -= fileLength;
				}
				else
				{
					lastFileEndIndex -= fileLength;
				}
			}

			long sum = 0;

			for (int i = 0; i < disk.Count; i++)
			{
				if (disk[i].Id != -1)
				{
					sum += disk[i].Id * i;
				}
			}

			Console.WriteLine($"New filesystem checksum {sum}");
		}
	}
}
