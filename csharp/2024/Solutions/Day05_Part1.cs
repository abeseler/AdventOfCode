﻿using Rule = (int before, int after);

namespace AdventOfCode.Solutions;

/// <summary>
/// https://adventofcode.com/2024/day/5
/// </summary>
internal sealed class Day05_Part1 : PuzzleSolution
{
    private const string DAY = "05";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "143";

    public static string Solve(StreamReader reader)
    {
        var sum = 0;
        var rules = new List<Rule>();

        while (reader.ReadLine() is { } line)
        {
            if (line.Length is 0) continue;

            if (line.IndexOf('|') is >= 0)
            {
                var seperator = line.IndexOf("|");
                var before = int.Parse(line[..seperator]);
                var after = int.Parse(line[(seperator + 1)..]);

                rules.Add((before, after));
                continue;
            }

            var parts = line.Split(",").Select(int.Parse).ToArray();
            var isMatch = true;

            foreach (var (before, after) in rules)
            {
                var beforeIndex = Array.IndexOf(parts, before);
                var afterIndex = Array.IndexOf(parts, after);

                if (beforeIndex == -1 || afterIndex == -1) continue;

                if (beforeIndex > afterIndex)
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                sum += parts[parts.Length / 2];
            }
        }

        return sum.ToString();
    }
}
