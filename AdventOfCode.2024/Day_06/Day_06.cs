﻿using System.Linq;
using System.Numerics;

namespace AdventOfCode2024;

internal static class Day_06
{
    private static readonly Vector2[] s_directions =
    [
        new(0, -1), // Up
        new(1, 0), // Right
        new(0, 1), // Down 
        new(-1, 0) // Left
    ];

    public static int Part_1(string filename)
    {
        var obstacles = new HashSet<Vector2>();
        var guardPosition = new Vector2(0, 0);
        var guardDirection = new Vector2(0, -1);
        var guardDirectionIndex = 0;
        var rows = 0;
        var columns = 0;

        using var reader = new StreamReader(filename);
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

        return path.Count;
    }

    public static int Part_2(string filename)
    {
        var obstacles = new HashSet<Vector2>();

        var startPosition = new Vector2(0, 0);
        var rows = 0;
        var columns = 0;

        using var reader = new StreamReader(filename);
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
                    startPosition = new Vector2(i, rows);
                }
            }

            columns = line.Length;
            rows++;
        }

        var path = new HashSet<Vector2>
        {
            startPosition
        };

        var guardPosition = startPosition;
        var guardDirectionIndex = 0;
        var guardDirection = s_directions[guardDirectionIndex];

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

        var redirects = new HashSet<(Vector2, Vector2)>();
        var validObstacleCandidates = new HashSet<Vector2>();

        for (var i = 0; i < path.Count; i++)
        {
            redirects.Clear();
            guardPosition = startPosition;
            guardDirectionIndex = 0;
            guardDirection = s_directions[guardDirectionIndex];

            var obstacleCandidate = path.ElementAt(i);
            if (obstacleCandidate == startPosition) continue;

            while (true)
            {
                var nextPosition = guardPosition + guardDirection;
                if (obstacleCandidate == nextPosition || obstacles.Contains(nextPosition))
                {
                    if (redirects.Contains((guardPosition, guardDirection)))
                    {
                        validObstacleCandidates.Add(obstacleCandidate);
                        break;
                    }
                    redirects.Add((guardPosition, guardDirection));

                    guardDirectionIndex = guardDirectionIndex == 3 ? 0 : guardDirectionIndex + 1;
                    guardDirection = s_directions[guardDirectionIndex];
                    continue;
                }
                if (nextPosition.X < 0 || nextPosition.X >= columns || nextPosition.Y < 0 || nextPosition.Y >= rows)
                {
                    break;
                }
                guardPosition = nextPosition;
            }
        }

        return validObstacleCandidates.Count;
    }
}
