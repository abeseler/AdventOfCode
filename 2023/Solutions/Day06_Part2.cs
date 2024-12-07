namespace AdventOfCode.Solutions;

internal sealed class Day06_Part2 : PuzzleSolution
{
    private const string DAY = "06";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "71503";

    public static string Solve(StreamReader reader)
    {
        var time = long.Parse(reader.ReadLine()!.Replace("Time:", "").Replace(" ", ""));
        var distance = long.Parse(reader.ReadLine()!.Replace("Distance:", "").Replace(" ", ""));

        var winningOptions = CalculateWaysToWin(time, distance);

        return winningOptions.ToString();
    }

    private static long CalculateWaysToWin(long time, long distance)
    {
        var wins = 0;

        for (long i = 1; i <= time - 1; i++)
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
