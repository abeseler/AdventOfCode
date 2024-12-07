namespace AdventOfCode2024;

internal sealed class Day02_Part2 : PuzzleSolution
{
    public static string FileName { get; } = "Day_02/Input.txt";
    public static string TestFileName { get; } = "Day_02/Example.txt";
    public static string TestOutputExpected { get; } = "4";

    public static string Solve(StreamReader reader)
    {
        var safeReports = 0;
        while (reader.ReadLine() is { } line)
        {
            var numbers = line.Split(' ').Select(int.Parse).ToArray();
            var reportIsSafe = false;

            for (var i = 0; i < numbers.Length; i++)
            {
                var numbersWithoutIndex = numbers.Where((_, index) => index != i);
                reportIsSafe = ReportIsSafe(numbersWithoutIndex.ToArray());
                if (reportIsSafe)
                {
                    safeReports++;
                    break;
                }
            }
        }

        return safeReports.ToString();
    }

    private static bool ReportIsSafe(int[] numbers)
    {
        var direction = Change.None;
        for (var i = 1; i < numbers.Length; i++)
        {
            var diff = numbers[i] - numbers[i - 1];
            var isWithinRange = diff != 0 && Math.Abs(diff) <= 3;
            if (isWithinRange is false)
            {
                return false;
            }
            if (direction == Change.None)
            {
                direction = diff > 0 ? Change.Increasing : Change.Decreasing;
            }
            if (direction == Change.Increasing && diff < 0)
            {
                return false;
            }
            if (direction == Change.Decreasing && diff > 0)
            {
                return false;
            }
        }
        return true;
    }

    private enum Change
    {
        None = 0,
        Increasing = 1,
        Decreasing = -1
    }
}
