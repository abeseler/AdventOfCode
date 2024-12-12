using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Solutions;

/// <summary>
/// https://adventofcode.com/2024/day/12
/// </summary>
internal sealed class Day12_Part1 : PuzzleSolution
{
    private const string DAY = "12";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "1930";
    public static string Solve(StreamReader reader)
    {
        var garden = new Garden();
        while (reader.ReadLine() is { } line)
        {
            for (var x = 0; x < line.Length; x++)
            {
                garden.GardenPlots[new Vector2(x, garden.Height)] = line[x];
            }

            garden.Width = line.Length;
            garden.Height++;
        }

        var regions = Regions(garden);        
        var totalPrice = regions.Sum(region => region.Price);

        return totalPrice.ToString();
    }

    private static List<GardenRegion> Regions(Garden garden)
    {
        var regions = new List<GardenRegion>();
        var visited = new HashSet<Vector2>();
        var queue = new Queue<Vector2>();

        foreach (var (location, plant) in garden.GardenPlots)
        {
            if (visited.Contains(location)) continue;
            queue.Enqueue(location);

            var neighborsWithDifferentPlants = new List<Vector2>();
            var region = new GardenRegion(plant);
            var queuedNeighbors = new HashSet<Vector2>
            {
                location
            };
            regions.Add(region);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                region.Locations.Add(current);
                visited.Add(current);

                if (current.X == 0) region.Perimeter++;
                if (current.X == garden.Width - 1) region.Perimeter++;
                if (current.Y == 0) region.Perimeter++;
                if (current.Y == garden.Height - 1) region.Perimeter++;

                foreach (var neighbor in current.Neighbors())
                {
                    if (garden.GardenPlots.TryGetValue(neighbor, out var neighborPlant))
                    {
                        if (neighborPlant != region.Plant)
                        {
                            neighborsWithDifferentPlants.Add(neighbor);
                            continue;   
                        }
                        else if (queuedNeighbors.Contains(neighbor))
                        {
                            continue;
                        }

                        queuedNeighbors.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
            region.Perimeter += neighborsWithDifferentPlants.Count;
        }

        return regions;
    }

    private sealed class Garden
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Dictionary<Vector2, char> GardenPlots { get; } = [];
    }

    private sealed record GardenRegion
    {
        public GardenRegion(char plant) => Plant = plant;
        public char Plant { get; }
        public int Perimeter { get; set; }
        public int Area => Locations.Count;
        public int Price => Area * Perimeter;
        public HashSet<Vector2> Locations { get; } = [];
    }
}
