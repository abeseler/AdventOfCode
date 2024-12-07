namespace AdventOfCode.Solutions;

internal sealed class Day05_Part1 : PuzzleSolution
{
    private const string DAY = "05";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "35";

    public static string Solve(StreamReader reader)
    {
        var seeds = new List<long>();
        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrEmpty(line)) break;
            seeds.AddRange(line.Split(' ').Where(s => long.TryParse(s, out _)).Select(long.Parse));
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

        var locations = new HashSet<long>();

        foreach (var seed in seeds)
        {
            var value = seed;

            for (var i = 0; i < maps.Count; i++)
            {
                map = (Maps)i;

                var mapValues = maps[map];
                foreach (var (start, end, adjustment) in mapValues)
                {
                    if (value >= start && value <= end)
                    {
                        value += adjustment;
                        break;
                    }
                }
            }

            locations.Add(value);
        }

        return locations.Min().ToString();
    }

    public enum Maps
    {
        SeedToSoil,
        SoilToFertilizer,
        FertilizerToWater,
        WaterToLight,
        LightToTemperature,
        TemperatureToHumidity,
        HumidityToLocation
    }
}
