namespace AdventOfCode.Solutions;

internal sealed class Day06_Part1 : PuzzleSolution
{
    private const string DAY = "06";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "288";

    public static string Solve(StreamReader reader)
    {
        int[] times = reader.ReadLine()!
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(c => char.IsDigit(c[0]))
            .Select(int.Parse)
            .ToArray();

        int[] distances = reader.ReadLine()!
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(c => char.IsDigit(c[0]))
            .Select(int.Parse)
            .ToArray();

        var winningOptions = 1;
        for (int i = 0; i < times.Length; i++)
        {
            winningOptions *= CalculateWaysToWin(times[i], distances[i]);
        }

        return winningOptions.ToString();
    }

    private static int CalculateWaysToWin(int time, int distance)
    {
        var wins = 0;

        for (int i = 1; i <= time - 1; i++)
        {
            var timeRemaining = time - i;
            var distanceTravelled = i * timeRemaining;
            if (distanceTravelled > distance)
            {
                wins++;
            }
        }

        return wins;
    }
}
