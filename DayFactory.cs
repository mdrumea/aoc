using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using AdventOfCode.Year2022.Day01;
using AdventOfCode.Year2022.Day02;
using AdventOfCode.Year2022.Day03;

namespace AdventOfCode
{
	internal static class DayFactory
	{
		public static IDayChallenge? GetDayChallenge(int year, int day)
		{
			var className = $"Day{day:D2}";
			var type = Assembly.GetExecutingAssembly().GetType($"AdventOfCode.Year{year}.{className}.Challenge");
			if (type != null && Activator.CreateInstance(type) is IDayChallenge instance)
			{
				return instance;
			}
			return null;
		}
	}
}
