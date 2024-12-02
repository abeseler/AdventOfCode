namespace AdventOfCode2024;

internal static class Day_02
{
    public static int Part_1(string filename)
    {
        var safeReports = 0;
        using var reader = new StreamReader(filename);
        while (reader.ReadLine() is { } line)
        {
            var numbers = line.Split(' ').Select(int.Parse).ToArray();
            var reportIsSafe = ReportIsSafe(numbers);
            if (reportIsSafe)
            {
                safeReports++;
            }
        }

        return safeReports;
    }

    public static int Part_2(string filename)
    {
        var safeReports = 0;
        using var reader = new StreamReader(filename);
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

        return safeReports;
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
