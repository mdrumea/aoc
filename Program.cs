namespace AdventOfCode
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var day = GetDayNumberFromConsole();

			Console.WriteLine($"\nRunning Day {day}..." + Environment.NewLine);

			var dayChallenge = DayFactory.GetDayChallenge(day);

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

		static int GetDayNumberFromConsole()
		{
			Console.Write("Enter a number: ");
			string? input = Console.ReadLine();
			if (int.TryParse(input, out int dayNumber))
			{
				if (dayNumber < 1 || dayNumber > 25)
				{
					Console.WriteLine("Invalid number entered.");
					return GetDayNumberFromConsole();
				}

				return dayNumber;
			}
			else
			{
				Console.WriteLine("Invalid number entered.");
				return GetDayNumberFromConsole();
			}
		}

	}
}
