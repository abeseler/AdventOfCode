using System.Numerics;

namespace AdventOfCode.Solutions;

internal sealed class Day08_Part2 : PuzzleSolution
{
    private const string DAY = "08";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "34";

    public static string Solve(StreamReader reader)
    {
        var antennas = new Dictionary<char, List<Antenna>>();

        var height = 0;
        var width = 0;
        while (reader.ReadLine() is { } line)
        {
            for (var col = 0; col < line.Length; col++)
            {
                var c = line[col];
                if (c == '.') continue;

                var antenna = new Antenna(new Vector2(col, height), c);
                if (antennas.ContainsKey(c) is false)
                {
                    antennas[c] = [];
                }
                antennas[c].Add(antenna);
            }
            width = line.Length;
            height++;
        }

        var antiNodes = new Dictionary<Vector2, AntiNodes>();

        foreach (var (frequency, antennaList) in antennas)
        {
            for (var i = 0; i < antennaList.Count; i++)
            {
                for (var j = i + 1; j < antennaList.Count; j++)
                {
                    var a = antennaList[i];
                    var b = antennaList[j];
                    var positions = CalculateAntiNodePositions(a, b, new(width, height));
                    foreach (var position in positions)
                    {
                        if (position.X < 0 || position.X >= width || position.Y < 0 || position.Y >= height)
                        {
                            continue;
                        }
                        if (antiNodes.ContainsKey(position) is false)
                        {
                            antiNodes[position] = new AntiNodes(position, []);
                        }
                        if (antiNodes[position].Frequencies.Contains(frequency) is false)
                        {
                            antiNodes[position].Frequencies.Add(frequency);
                        }
                    }
                }
            }
        }

        var locations = antiNodes.Keys.ToHashSet();
        foreach (var antennaList in antennas.Values)
        {
            foreach (var antenna in antennaList)
            {
                locations.Add(antenna.Location);
            }
        }

        return locations.Count.ToString();
    }

    private static IEnumerable<Vector2> CalculateAntiNodePositions(Antenna a, Antenna b, Vector2 bounds)
    {
        var step = b.Location - a.Location;
        var accumulator = step;
        while (b.Location + accumulator is { } next && next.X >= 0 && next.X <= bounds.X && next.Y >= 0 && next.Y <= bounds.Y)
        {
            accumulator += step;
            yield return next;
        }
        step = a.Location - b.Location;
        accumulator = step;
        while (a.Location + accumulator is { } next && next.X >= 0 && next.X <= bounds.X && next.Y >= 0 && next.Y <= bounds.Y)
        {
            accumulator += step;
            yield return next;
        }
    }

    private sealed record Antenna(Vector2 Location, char Frequency)
    {

    }

    private sealed record AntiNodes(Vector2 Location, HashSet<char> Frequencies)
    {

    }
}
