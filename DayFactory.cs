using System;
using System.Collections.Generic;
using AdventOfCode.Year2022.Day01;

namespace AdventOfCode
{
    internal static class DayFactory
    {
        private static readonly Dictionary<int, Func<IDayChallenge>> dayChallenges = new()
        {
            { 1, () => new Day01() }
            // Add more days here as needed
        };

        public static IDayChallenge? GetDayChallenge(int day)
        {
            if (dayChallenges.TryGetValue(day, out var createDayChallenge))
            {
                return createDayChallenge();
            }
            return null;
        }
    }
}
