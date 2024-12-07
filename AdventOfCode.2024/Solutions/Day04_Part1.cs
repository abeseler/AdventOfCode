using System.Numerics;

namespace AdventOfCode2024.Solutions;

internal sealed partial class Day04_Part1 : PuzzleSolution
{
    public static string Name { get; } = "Day 04 Part 1";
    public static string FileName { get; } = "Data/04.input";
    public static string TestFileName { get; } = "Data/04.sample";
    public static string TestOutputExpected { get; } = "18";

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
                if (character is 'X')
                {
                    pointsToCheck.Add(point);
                }
            }

            row++;
        }

        var matches = 0;
        ReadOnlySpan<char> expected = ['M', 'A', 'S'];
        ReadOnlySpan<Vector2> directions =
        [
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(-1, 0),
            new Vector2(0, -1),
            new Vector2(1, 1),
            new Vector2(-1, 1),
            new Vector2(1, -1),
            new Vector2(-1, -1),
        ];

        foreach (var point in pointsToCheck)
        {
            foreach (var direction in directions)
            {
                var currentPoint = point;
                var found = true;
                foreach (var expectedChar in expected)
                {
                    currentPoint += direction;
                    if (!map.TryGetValue(currentPoint, out var character) || character != expectedChar)
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    matches++;
                }
            }
        }

        return matches.ToString();
    }
}
