using System.Numerics;

namespace AdventOfCode2024;

internal sealed partial class Day06_Part1 : PuzzleSolution
{
    public static string FileName { get; } = "Day_06/Input.txt";
    public static string TestFileName { get; } = "Day_06/Example.txt";
    public static string TestOutputExpected { get; } = "41";

    private static readonly Vector2[] s_directions =
    [
        new(0, -1), // Up
        new(1, 0), // Right
        new(0, 1), // Down 
        new(-1, 0) // Left
    ];

    public static string Solve(StreamReader reader)
    {
        var obstacles = new HashSet<Vector2>();
        var guardPosition = new Vector2(0, 0);
        var guardDirection = new Vector2(0, -1);
        var guardDirectionIndex = 0;
        var rows = 0;
        var columns = 0;

        while (reader.ReadLine() is { } line)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '#')
                {
                    obstacles.Add(new Vector2(i, rows));
                }
                else if (line[i] == '^')
                {
                    guardPosition = new Vector2(i, rows);
                }
            }

            columns = line.Length;
            rows++;
        }

        var path = new HashSet<Vector2>
        {
            guardPosition
        };

        while (true)
        {
            var nextPosition = guardPosition + guardDirection;
            if (obstacles.Contains(nextPosition))
            {
                guardDirectionIndex = guardDirectionIndex == 3 ? 0 : guardDirectionIndex + 1;
                guardDirection = s_directions[guardDirectionIndex];
                continue;
            }

            if (nextPosition.X < 0 || nextPosition.X >= columns || nextPosition.Y < 0 || nextPosition.Y >= rows)
            {
                break;
            }

            guardPosition = nextPosition;
            path.Add(guardPosition);
        }

        return path.Count.ToString();
    }
}
