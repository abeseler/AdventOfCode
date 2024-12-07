namespace AdventOfCode.Solutions;

internal sealed class Day01_Part2 : PuzzleSolution
{
    private const string DAY = "01";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "31";

    public static string Solve(StreamReader reader)
    {
        var leftValues = new List<int>();
        var rightValues = new Dictionary<int, int>();
        while (reader.ReadLine() is { } line)
        {
            var firstspace = line.IndexOf(' ');
            var lastspace = line.LastIndexOf(' ');
            var left = int.Parse(line[..firstspace]);
            var right = int.Parse(line[(lastspace + 1)..]);
            leftValues.Add(left);
            if (rightValues.TryGetValue(right, out int value))
            {
                rightValues[right] = ++value;
            }
            else
            {
                rightValues[right] = 1;
            }
        }

        var similarity = 0;

        foreach (var left in leftValues)
        {
            if (rightValues.TryGetValue(left, out int value))
            {
                similarity += value * left;
            }
        }

        return similarity.ToString();
    }
}
