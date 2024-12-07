namespace AdventOfCode.Solutions;

internal sealed class Day01_Part1 : PuzzleSolution
{
    private const string DAY = "01";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "11";

    public static string Solve(StreamReader reader)
    {
        var leftValues = new List<int>();
        var rightValues = new List<int>();

        while (reader.ReadLine() is { } line)
        {
            var firstspace = line.IndexOf(' ');
            var lastspace = line.LastIndexOf(' ');
            leftValues.Add(int.Parse(line[..firstspace]));
            rightValues.Add(int.Parse(line[(lastspace + 1)..]));
        }

        leftValues.Sort();
        rightValues.Sort();

        var distance = 0;
        for (var i = 0; i < leftValues.Count; i++)
        {
            var dist = Math.Abs(leftValues[i] - rightValues[i]);
            distance += dist;
        }

        return distance.ToString();
    }
}
