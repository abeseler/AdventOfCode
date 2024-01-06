using System.Buffers;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AdventOfCode_2023.Day_5;

internal static class AlmanacDemystifier
{
    private static readonly SearchValues<char> _digits = SearchValues.Create("1234567890");

    public static long GetLowestSeedLocation(string fileName)
    {
        var seeds = new List<Seed>();
        var mapCategories = new Category[7];

        for (var i = 0; i < mapCategories.Length; i++)
            mapCategories[i] = new(1);

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

            mapCategories[mapIndex].Maps.Add(GetMap(line));
        }

        var minLocation = long.MaxValue;
        var seedsAsSpan = CollectionsMarshal.AsSpan(seeds);
        for (var i = 0; i < seedsAsSpan.Length; i++)
        {
            var input = seedsAsSpan[i].Value;
            foreach (var category in mapCategories)
            {
                var maps = CollectionsMarshal.AsSpan(category.Maps);
                foreach (var map in CollectionsMarshal.AsSpan(category.Maps))
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
    public static long GetLowestSeedLocationWithRanges(string fileName)
    {
        var seedRanges = new List<SeedRange>();
        var mapCategories = new Category[7];

        for (var i = 0; i < mapCategories.Length; i++)
            mapCategories[i] = new(1);

        var mapIndex = -1;

        using var reader = new StreamReader(fileName);
        while (reader.ReadLine() is { } line)
        {
            if (seedRanges.Count == 0)
            {
                PopulateSeedRanges(seedRanges, line);
                continue;
            }
            if (line.Length == 0)
            {
                mapIndex++;
                continue;
            }
            if (!_digits.Contains(line[0]))
                continue;

            mapCategories[mapIndex].Maps.Add(GetMap(line));
        }

        long location = 1;
        var seedRangesAsSpan = CollectionsMarshal.AsSpan(seedRanges);
        while (true)
        {
            var result = location;
            for (var i = mapCategories.Length - 1; i >= 0; i--)
            {
                var maps = CollectionsMarshal.AsSpan(mapCategories[i].Maps);
                result = GetReverseMapping(result, maps);
            }
            if (IsInRange(result, seedRangesAsSpan))
                return location;

            location++;
        }
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
    private static void PopulateSeedRanges(List<SeedRange> seedRanges, ReadOnlySpan<char> line)
    {
        var digitStart = 7;
        long start = -1;
        for (var i = digitStart + 1; i < line.Length; i++)
        {
            if (line[i] == ' ' || i == line.Length - 1)
            {
                long value;
                if (i == line.Length - 1)
                    value = long.Parse(line[digitStart..]);
                else
                    value = long.Parse(line[digitStart..i]);

                digitStart = i + 1;

                if (start == -1)
                    start = value;
                else
                {
                    seedRanges.Add(new(start, value));
                    start = -1;
                }
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
    private static bool IsInRange(long value, Span<SeedRange> seedRanges)
    {
        foreach (var seedRange in seedRanges)
        {
            if (seedRange.IsInRange(value))
                return true;
        }
        return false;
    }
    private static long GetReverseMapping(long input, Span<Map> maps)
    {
        var returnValue = input;
        foreach (var map in maps)
        {
            if (map.TryGetSource(ref returnValue))
                break;
        }
        return returnValue;
    }
    private struct Map(long destinationStart, long sourceStart, long range)
    {
        public long MinSource = sourceStart;
        public long MaxSource = sourceStart + range - 1;
        public long MinDestination = destinationStart;
        public long MaxDestination = destinationStart + range - 1;
        public long DestinationIncrement = destinationStart - sourceStart;
        public readonly bool TryGetRemapping(ref long input)
        {
            if (input < MinSource || input > MaxSource)
                return false;

            input = DestinationIncrement + input;
            return true;
        }
        public readonly bool TryGetSource(ref long input)
        {
            if (input < MinDestination || input > MaxDestination)
                return false;

            input -= DestinationIncrement;
            return true;
        }
    }
    private struct Category(int id)
    {
        public readonly int Id = id;
        public List<Map> Maps = [];
    }
    private struct Seed(long value)
    {
        public long Value { get; set; } = value;
        public long Location { get; set; } = value;
    }
    private struct SeedRange(long start, long range, long increment = 0)
    {
        public long Min = start;
        public long Max = start + range - 1;
        public long Increment = increment;
        public readonly bool IsInRange(long value) => value >= Min && value <= Max;
    }
}
