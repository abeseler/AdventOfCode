namespace AdventOfCode.Solutions;

internal sealed class Day09_Part2 : PuzzleSolution
{
    private const string DAY = "09";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "2";

    public static string Solve(StreamReader reader)
    {
        var sum = 0;
        while (reader.ReadLine() is { } line)
        {
            var sequence = line.Split(' ').Select(int.Parse).Reverse().ToArray();
            var next = NextNumber(sequence, false);
            sum += next;
        }

        return sum.ToString();
    }

    private static int NextNumber(int[] sequence, bool done)
    {
        if (done)
        {
            return 0;
        }

        var diffs = new int[sequence.Length - 1];
        for (var i = 0; i < sequence.Length - 1; i++)
        {
            diffs[i] = sequence[i + 1] - sequence[i];
        }

        return sequence.Last() + NextNumber(diffs, diffs.All(x => x == 0));
    }
}
