﻿namespace AdventOfCode.Solutions;

/// <summary>
/// https://adventofcode.com/2024/day/2
/// </summary>
internal sealed class Day02_Part1 : PuzzleSolution
{
    private const string DAY = "02";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "2";

    public static string Solve(StreamReader reader)
    {
        var safeReports = 0;
        while (reader.ReadLine() is { } line)
        {
            var numbers = line.Split(' ').Select(int.Parse).ToArray();
            var reportIsSafe = ReportIsSafe(numbers);
            if (reportIsSafe)
            {
                safeReports++;
            }
        }

        return safeReports.ToString();
    }

    private static bool ReportIsSafe(int[] numbers)
    {
        var direction = Change.None;
        for (var i = 1; i < numbers.Length; i++)
        {
            var diff = numbers[i] - numbers[i - 1];
            var isWithinRange = diff != 0 && Math.Abs(diff) <= 3;
            if (isWithinRange is false)
            {
                return false;
            }
            if (direction == Change.None)
            {
                direction = diff > 0 ? Change.Increasing : Change.Decreasing;
            }
            if (direction == Change.Increasing && diff < 0)
            {
                return false;
            }
            if (direction == Change.Decreasing && diff > 0)
            {
                return false;
            }
        }
        return true;
    }

    private enum Change
    {
        None = 0,
        Increasing = 1,
        Decreasing = -1
    }
}
