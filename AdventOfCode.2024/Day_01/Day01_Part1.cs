namespace AdventOfCode2024;

internal sealed class Day01_Part1 : PuzzleSolution
{
    public static string FileName { get; } = "Day_01/Input.txt";
    public static string TestFileName { get; } = "Day_01/Example.txt";
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
