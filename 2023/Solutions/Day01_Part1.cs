using System.Buffers;

namespace AdventOfCode.Solutions;

internal sealed class Day01_Part1 : PuzzleSolution
{
    private const string DAY = "01";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "142";

    private static readonly SearchValues<char> _digits = SearchValues.Create("123456789");

    public static string Solve(StreamReader reader)
    {
        var sum = 0;
        while (reader.ReadLine() is { } line)
        {
            sum += GetFirstDigit(line) * 10 + GetLastDigit(line);
        }
        return sum.ToString();
    }

    private static int GetFirstDigit(in ReadOnlySpan<char> span)
    {
        for (int i = 0; i < span.Length; i++)
        {
            if (_digits.Contains(span[i]))
                return span[i] - '0';
        }
        return 0;
    }
    private static int GetLastDigit(in ReadOnlySpan<char> span)
    {
        for (int i = span.Length - 1; i >= 0; i--)
        {
            if (_digits.Contains(span[i]))
                return span[i] - '0';
        }
        return 0;
    }
}
