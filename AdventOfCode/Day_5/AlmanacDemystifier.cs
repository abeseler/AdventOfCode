using System.Buffers;
using System.Runtime.InteropServices;

namespace AdventOfCode_2023.Day_5;

internal static class AlmanacDemystifier
{
    private static readonly SearchValues<char> _digits = SearchValues.Create("1234567890");

    public static long GetLowestSeedLocation(string fileName)
    {
        var seeds = new List<Seed>();
        var mapCategories = new KeyValuePair<long, List<Map>>[7];

        for (var i = 0; i < mapCategories.Length; i++)
            mapCategories[i] = new KeyValuePair<long, List<Map>>(i, []);

        var mapIndex = -1;

        using var reader = new StreamReader(fileName);
        while (reader.ReadLine() is { } line)
        {
            if (seeds.Count == 0)
            {
                PopulateSeeds(seeds, line);
                continue;
            }
            if (line.Length == 0)
            {
                mapIndex++;
                continue;
            }
            if (!_digits.Contains(line[0]))
                continue;

            mapCategories[mapIndex].Value.Add(GetMap(line));
        }

        var minLocation = long.MaxValue;
        var seedsAsSpan = CollectionsMarshal.AsSpan(seeds);
        for (var i = 0; i < seedsAsSpan.Length; i++)
        {
            var input = seedsAsSpan[i].Value;
            foreach (var category in mapCategories)
            {
                var maps = CollectionsMarshal.AsSpan(category.Value);
                foreach (var map in maps)
                {
                    if (map.TryGetRemapping(ref input))
                        break;
                }
            }
            seedsAsSpan[i].Location = input;
            minLocation = Math.Min(minLocation, input);
        }

        return minLocation;
    }

    private static void PopulateSeeds(List<Seed> seeds, ReadOnlySpan<char> line)
    {
        var digitStart = 7;
        for (var i = digitStart + 1; i < line.Length; i++)
        {
            if (line[i] == ' ' || i == line.Length - 1)
            {
                if (i == line.Length - 1)
                    seeds.Add(new(long.Parse(line[digitStart..])));
                else
                    seeds.Add(new(long.Parse(line[digitStart..i])));

                digitStart = i + 1;
            }
        }
    }
    private static Map GetMap(ReadOnlySpan<char> line)
    {
        var firstSpace = line.IndexOf(' ');
        var lastSpace = line.LastIndexOf(' ');

        var destination = long.Parse(line[..(firstSpace)]);
        var source = long.Parse(line[(firstSpace + 1)..lastSpace]);
        var range = long.Parse(line[(lastSpace + 1)..]);

        return new Map(destination, source, range);
    }

    private struct Map(long destinationStart, long sourceStart, long range)
    {
        public long MinSource = sourceStart;
        public long MaxSource = sourceStart + range;
        public long DestinationIncrement = destinationStart - sourceStart;
        public readonly bool TryGetRemapping(ref long input)
        {
            if (input < MinSource || input > MaxSource)
                return false;

            input = DestinationIncrement + input;
            return true;
        }
    }
    private struct Seed(long value)
    {
        public long Value { get; set; } = value;
        public long Location { get; set; } = value;
    }
}
