using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Solutions;

/// <summary>
/// https://adventofcode.com/2024/day/6#part2
/// </summary>
internal sealed class Day10_Part2 : PuzzleSolution
{
    private const string DAY = "10";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "81";
    public static string Solve(StreamReader reader)
    {
        var map = new Dictionary<Vector2, Terrain>();
        var trailHeads = new List<Terrain>();

        var y = 0;
        while (reader.ReadLine() is { } line)
        {
            var lineSpan = line.AsSpan();
            for (var x = 0; x < lineSpan.Length; x++)
            {
                var elevation = lineSpan[x] - '0';
                var location = new Vector2(x, y);
                var terrain = new Terrain { Location = location, Elevation = elevation };
                map[location] = terrain;
                if (terrain.IsTrailHead)
                {
                    trailHeads.Add(terrain);
                }
            }
            y++;
        }

        var trailRatings = 0;
        foreach (var trailHead in trailHeads)
        {
            trailRatings += CalculateTrailRating(trailHead, map);
        }

        return trailRatings.ToString();
    }

    private static int CalculateTrailRating(Terrain trailHead, Dictionary<Vector2, Terrain> map)
    {
        var rating = 0;

        var visited = new HashSet<Vector2>();
        var queue = new Queue<Terrain>();
        queue.Enqueue(trailHead);

        do
        {
            var current = queue.Dequeue();
            visited.Add(current.Location);
            if (current.IsPeak)
            {
                rating++;
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
                    queue.Enqueue(neighbor);
                }
            }
        }
        while (queue.Count > 0);

        return rating;
    }

    private static IEnumerable<Terrain> GetPassableNeighbors(Terrain current, Dictionary<Vector2, Terrain> map)
    {
        foreach (var neighbor in current.Location.Neighbours())
        {
            if (map.TryGetValue(neighbor, out var terrain) && terrain.IsPassable(current))
            {
                yield return terrain;
            }
        }
    }

    private sealed record Terrain
    {
        public Vector2 Location { get; init; }
        public int Elevation { get; init; }
        public bool IsTrailHead => Elevation == 0;
        public bool IsPeak => Elevation == 9;
        public bool IsPassable(Terrain origin) => origin.Elevation == Elevation - 1;
    }
}

