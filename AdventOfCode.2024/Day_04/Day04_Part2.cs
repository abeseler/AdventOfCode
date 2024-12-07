using System.Numerics;

namespace AdventOfCode2024;

internal sealed partial class Day04_Part2 : PuzzleSolution
{
    public static string FileName { get; } = "Day_04/Input.txt";
    public static string TestFileName { get; } = "Day_04/Example.txt";
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
            if (map.TryGetValue(point + new Vector2(-1, -1), out var upleft)
                && upleft is 'M' or 'S'
                && map.TryGetValue(point + new Vector2(1, 1), out var downright)
                && downright is 'M' or 'S'
                && upleft != downright
                && map.TryGetValue(point + new Vector2(1, -1), out var upright)
                && upright is 'M' or 'S'
                && map.TryGetValue(point + new Vector2(-1, 1), out var downleft)
                && downleft is 'M' or 'S'
                && upright != downleft)
            {
                matches++;
            }
        }

        return matches.ToString();
    }
}
