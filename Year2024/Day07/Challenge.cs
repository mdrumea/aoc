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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Year2024.Day07
{
	internal class Challenge : BaseDayChallenge, IDayChallenge
	{
		public void Part1(Source source)
		{
			var lines = LoadSource(source);

			long sum = 0;


			foreach (var line in lines)
			{

				List<string> ops = new List<string>() { "+", "*" };
				List<string> expressions = new List<string>();

				var parts = line.Split(":");
				long resultX = long.Parse(parts[0]);

				var numbers = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
				FindExpressions(numbers, ops, 0, "", expressions);

				foreach (var expression in expressions)
				{
					long result = Parse(expression);

					if (result == resultX)
					{
						sum += resultX;
						break;
					}
				}
			}
			Console.WriteLine($"Calibration result: {sum}");

		}

		long Parse(string expression)
		{
			string pattern = @"\b\d+\b";
			MatchCollection matches = Regex.Matches(expression, pattern);

			var temp = expression;

			long result = int.Parse(matches.First().Value);
			temp = temp.Substring(matches.First().Value.Length);
			for (int i = 1;i < matches.Count; i++)
			{
				if (temp.Substring(0, 1) == "+")
				{
					result += int.Parse(matches[i].Value);
				}
				else
				{
					result *= int.Parse(matches[i].Value);
				}
				temp = temp.Substring(matches[i].Value.Length + 1);
			}

			return result;
		}

		public void FindExpressions(int[] arr, List<string> operations, int index, string currentExpression, List<string> allExpressions)
		{

			if (index == arr.Length - 1)
			{
				currentExpression += arr[index];
				allExpressions.Add(currentExpression);
				return;
			}


			foreach (var operation in operations)
			{
				string newExpression = currentExpression + arr[index] + operation;
				FindExpressions(arr, operations, index + 1, newExpression, allExpressions);
			}
		}

		public void Part2(Source source)
		{
			var lines = LoadSource(source);

			long sum = 0;


			foreach (var line in lines)
			{

				List<string> ops = new List<string>() { "+", "*", "||" };
				List<string> expressions = new List<string>();


				var parts = line.Split(":");
				long resultX = long.Parse(parts[0]);

				var numbers = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
				FindExpressions(numbers, ops, 0, "", expressions);

				foreach (var expression in expressions)
				{
					long result = ParseWithPipes(expression);

					if (result == resultX)
					{
						sum += resultX;
						break;
					}
				}
			}
			Console.WriteLine($"New calibration result: {sum}");
		}

		long ParseWithPipes(string expression)
		{
			var temp = expression;

			string pattern = @"\b\d+\b";
			MatchCollection matches = Regex.Matches(temp, pattern);

			long result = int.Parse(matches.First().Value);
			temp = temp.Substring(matches.First().Value.Length);
			for (int i = 1; i < matches.Count; i++)
			{
				if (temp.Substring(0, 1) == "+")
				{
					result += int.Parse(matches[i].Value);
					temp = temp.Substring(matches[i].Value.Length + 1);
				}
				else if (temp.Substring(0, 1) == "*")
				{

					result *= int.Parse(matches[i].Value);
					temp = temp.Substring(matches[i].Value.Length + 1);
				}
				else
				{
					result = long.Parse(result.ToString() + matches[i].Value);
					temp = temp.Substring(matches[i].Value.Length + 2);
				}
			}

			return result;
		}

	}
}
