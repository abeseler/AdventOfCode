using System.Diagnostics;

namespace AdventOfCode.Solutions;

internal sealed class Day05_Part2 : PuzzleSolution
{
    private const string DAY = "05";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "46";

    public static string Solve(StreamReader reader)
    {
        var seedCollection = new List<SeedRanges>();
        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrEmpty(line)) break;
            var values = line.Split(' ').Where(s => long.TryParse(s, out _)).Select(long.Parse).ToArray();

            for (var i = 0; i <= values.Length - 1; i += 2)
            {
                seedCollection.Add(new(values[i], values[i] + values[i + 1] - 1));
            }
        }

        var maps = new Dictionary<Maps, List<(long start, long end, long adjustment)>>();
        var map = Maps.SeedToSoil;
        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrEmpty(line))
            {
                map++;
                continue;
            }

            if (char.IsDigit(line[0]) is false) continue;

            if (maps.ContainsKey(map) is false)
            {
                maps[map] = [];
            }

            var values = line.Split(' ').Select(long.Parse).ToArray();

            var start = values[1];
            var end = values[1] + values[2] - 1;
            var adjustment = values[0] - values[1];

            maps[map].Add((start, end, adjustment));
        }

        for (var i = 0; i < maps.Count; i++)
        {
            map = (Maps)i;
            var current = maps[map];

            foreach (var seedRanges in seedCollection)
            {
                ApplyMap(seedRanges, map, current);
            }
        }

        var minLocation = seedCollection.Min(s => s.Min);

        return minLocation.ToString();
    }

    private static void ApplyMap(SeedRanges seedRanges, Maps map, List<(long start, long end, long adjustment)> maps)
    {
        while (seedRanges.Ranges.FirstOrDefault(s => s.MapApplied != map) is { } range)
        {
            foreach (var (start, end, adjustment) in maps)
            {
                if (range.MapApplied == map) break;
                if (range.ContainedWithin(start, end))
                {
                    range.Start += adjustment;
                    range.End += adjustment;
                    range.MapApplied = map;
                    break;
                }
                if (range.DoesNotIntersect(start, end)) continue;

                if (range.Start < start)
                {
                    var newRange = new SeedRange(range.Start, start - 1, range.MapApplied);
                    range.Start = start;
                    seedRanges.Ranges.Add(newRange);
                }

                if (range.End > end)
                {
                    var newRange = new SeedRange(end + 1, range.End, range.MapApplied);
                    range.End = end;
                    seedRanges.Ranges.Add(newRange);
                }

                range.Start += adjustment;
                range.End += adjustment;
                range.MapApplied = map;
            }

            range.MapApplied = map;
        }
    }

    private sealed class SeedRanges
    {
        public SeedRanges(long start, long end)
        {
            Ranges.Add(new(start, end, Maps.None));
        }
        public List<SeedRange> Ranges { get; } = [];
        public Maps? LastApplied => Ranges.Min(s => s.MapApplied);
        public long Min => Ranges.Min(s => s.Start);
    }

    [DebuggerDisplay("{DebuggerDisplay}")]
    private sealed record SeedRange
    {
        public SeedRange(long start, long end, Maps mapApplied)
        {
            Start = start;
            End = end;
            MapApplied = mapApplied;
        }
        public long Start { get; set; }
        public long End { get; set; }
        public Maps MapApplied { get; set; }
        public bool DoesNotIntersect(long start, long end)
        {
            return Start > end || End < start;
        }
        public bool ContainedWithin(long start, long end)
        {
            return Start >= start && End <= end;
        }
        public string DebuggerDisplay => $"Start: {Start}, End: {End}, MapApplied: {MapApplied}";
    }

    public enum Maps
    {
        None = -1,
        SeedToSoil,
        SoilToFertilizer,
        FertilizerToWater,
        WaterToLight,
        LightToTemperature,
        TemperatureToHumidity,
        HumidityToLocation
    }
}
