namespace AdventOfCode
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var year = GetYearNumberFromConsole();
			var day = GetDayNumberFromConsole();

			Console.WriteLine($"\nRunning Day {day} of {year}..." + Environment.NewLine);

			var dayChallenge = DayFactory.GetDayChallenge(year, day);

			if (dayChallenge == null)
			{
				Console.WriteLine("Day not implemented yet.");
				return;
			}

			Console.WriteLine("Running example input...");
			DurationTimer(() => dayChallenge.Part1(Source.Sample));

			Console.WriteLine("Running part 1...");
			DurationTimer(() => dayChallenge.Part1(Source.Input));

			Console.WriteLine("Running example input...");
			DurationTimer(() => dayChallenge.Part2(Source.Sample));

			Console.WriteLine("Running part 2...");
			DurationTimer(() => dayChallenge.Part2(Source.Input));
		}

		private static void DurationTimer(Action action)
		{
			var watch = System.Diagnostics.Stopwatch.StartNew();

			action();

			watch.Stop();
			Console.WriteLine($"Executed in {(watch.ElapsedMilliseconds / 1000.0):N3} sec" + Environment.NewLine);
		}

		static int GetYearNumberFromConsole()
		{
			Console.Write("Enter a year: ");
			string? input = Console.ReadLine();
			if (int.TryParse(input, out int year))
			{
				if (year < 2015 && year > 2024)
				{
					Console.WriteLine("Invalid year entered.");
					return GetYearNumberFromConsole();
				}

				return year;
			}
			else
			{
				Console.WriteLine("Invalid number entered.");
				return GetYearNumberFromConsole();
			}
		}

		static int GetDayNumberFromConsole()
		{
			Console.Write("Enter a day: ");
			string? input = Console.ReadLine();
			if (int.TryParse(input, out int day))
			{
				if (day < 1 || day > 25)
				{
					Console.WriteLine("Invalid number entered.");
					return GetDayNumberFromConsole();
				}

				return day;
			}
			else
			{
				Console.WriteLine("Invalid number entered.");
				return GetDayNumberFromConsole();
			}
		}

	}
}
