﻿using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Solutions;

/// <summary>
/// https://adventofcode.com/2024/day/4#part2
/// </summary>
internal sealed class Day04_Part2 : PuzzleSolution
{
    private const string DAY = "04";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "9";

    public static string Solve(StreamReader reader)
    {
        var map = new Dictionary<Vector2, char>();
        var pointsToCheck = new List<Vector2>();
        var row = 0;
        while (reader.ReadLine() is { } line)
        {
            for (var col = 0; col < line.Length; col++)
            {
                var point = new Vector2(col, row);
                var character = line[col];
                map.Add(point, character);
                if (character is 'A')
                {
                    pointsToCheck.Add(point);
                }
            }

            row++;
        }

        var matches = 0;

        foreach (var point in pointsToCheck)
        {
            if (map.TryGetValue(point.UpLeft(), out var upleft)
                && upleft is 'M' or 'S'
                && map.TryGetValue(point.DownRight(), out var downright)
                && downright is 'M' or 'S'
                && upleft != downright
                && map.TryGetValue(point.UpRight(), out var upright)
                && upright is 'M' or 'S'
                && map.TryGetValue(point.DownLeft(), out var downleft)
                && downleft is 'M' or 'S'
                && upright != downleft)
            {
                matches++;
            }
        }

        return matches.ToString();
    }
}
