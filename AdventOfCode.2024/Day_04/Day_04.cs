using System.Drawing;
using System.Numerics;

namespace AdventOfCode2024;

internal static class Day_04
{
    public static int Part_1(string filename)
    {        
        var map = new Dictionary<Vector2, char>();
        var pointsToCheck = new List<Vector2>();
        var row = 0;
        using var reader = new StreamReader(filename);
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

        return matches;
    }

    public static int Part_2(string filename)
    {
        var map = new Dictionary<Vector2, char>();
        var pointsToCheck = new List<Vector2>();
        var row = 0;
        using var reader = new StreamReader(filename);
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

        return matches;
    }
}
