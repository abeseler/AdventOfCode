﻿using System.Text.RegularExpressions;

namespace AdventOfCode;

/// <summary>
/// https://adventofcode.com/2024/day/3#part2
/// </summary>
internal sealed partial class Day03_Part2 : PuzzleSolution
{
    private const string DAY = "03";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample2";
    public static string TestOutputExpected { get; } = "48";

    public static string Solve(StreamReader reader)
    {
        var sum = 0;
        var enabled = true;
        while (reader.ReadLine() is { } line)
        {
            foreach (var match in GetInstructions().EnumerateMatches(line))
            {
                var span = line.AsSpan()[(match.Index)..(match.Index + match.Length)];
                if (span[0] == 'd' && match.Length == 4)
                {
                    enabled = true;
                }
                else if (span[0] == 'd' && match.Length == 7)
                {
                    enabled = false;
                }
                else if (enabled)
                {
                    var commaIndex = span.IndexOf(',');
                    var a = int.Parse(span[4..commaIndex]);
                    var b = int.Parse(span.Slice(commaIndex + 1, span.Length - commaIndex - 2));
                    sum += a * b;
                }
            }
        }

        return sum.ToString();
    }

    [GeneratedRegex(@"(?:do|don't)\(\)|mul\(\d{1,3},\d{1,3}\)")]
    public static partial Regex GetInstructions();
}
