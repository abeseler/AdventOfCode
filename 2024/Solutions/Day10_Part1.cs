using System.Drawing;
using System.Numerics;

namespace AdventOfCode.Solutions;

internal sealed class Day10_Part1 : PuzzleSolution
{
    private const string DAY = "10";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "36";
    public static string Solve(StreamReader reader)
    {
        var map = new Dictionary<Point, Terrain>();
        var trailHeads = new List<Terrain>();

        var y = 0;
        while (reader.ReadLine() is { } line)
        {
            var lineSpan = line.AsSpan();
            for (var x = 0; x < lineSpan.Length; x++)
            {
                var elevation = lineSpan[x] - '0';
                var location = new Point(x, y);
                var terrain = new Terrain { Location = location, Elevation = elevation };
                map[location] = terrain;
                if (terrain.IsTrailHead)
                {
                    trailHeads.Add(terrain);
                }
            }
            y++;
        }

        var trailScores = 0;
        foreach (var trailHead in trailHeads)
        {
            trailScores += CalculateTrailScore(trailHead, map);
        }

        return trailScores.ToString();
    }

    private static int CalculateTrailScore(Terrain trailHead, Dictionary<Point, Terrain> map)
    {
        var score = 0;
        var visited = new HashSet<Point>();
        var queue = new Queue<Terrain>();
        queue.Enqueue(trailHead);

        do {
            var current = queue.Dequeue();
            if (current.IsPeak)
            {
                score++;
                continue;
            }
            foreach (var neighbor in GetPassableNeighbors(current, map))
            {
                if (visited.Contains(neighbor.Location))
                {
                    continue;
                }
                else
                {
                    visited.Add(neighbor.Location);
                    queue.Enqueue(neighbor);
                }
            }
        }
        while (queue.Count > 0);

        return score;
    }

    private static IEnumerable<Terrain> GetPassableNeighbors(Terrain current, Dictionary<Point, Terrain> map)
    {
        foreach (var direction in Directions)
        {
            var next = current.Location.Add(direction);
            if (map.TryGetValue(next, out var neighbor) && neighbor.IsPassable(current))
            {
                yield return neighbor;
            }
        }
    }

    private sealed record Terrain
    {
        public Point Location { get; init; }
        public int Elevation { get; init; }
        public bool IsTrailHead => Elevation == 0;
        public bool IsPeak => Elevation == 9;
        public bool IsPassable(Terrain origin) => origin.Elevation == Elevation - 1;
    }

    private static readonly Point[] Directions =
    [
        new(0, -1), // Up
        new(1, 0), // Right
        new(0, 1), // Down
        new(-1, 0), // Left
    ];
}

file static class PointExt
{
    public static Point Add(this Point point, Point other) => new(point.X + other.X, point.Y + other.Y);
}
