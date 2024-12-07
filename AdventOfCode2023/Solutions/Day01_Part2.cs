using System.Buffers;

namespace AdventOfCode2024.Solutions;

internal sealed class Day01_Part2 : PuzzleSolution
{
    public static string Name { get; } = "Day 01 Part 2";
    public static string FileName { get; } = "Data/01.input";
    public static string TestFileName { get; } = "Data/01.sample2";
    public static string TestOutputExpected { get; } = "281";

    private static readonly KeyValuePair<int, ValueTuple<string, int>[]>[] _tokens =
    [
        new(2, [("one", 1), ("two", 2), ("six", 6)]),
        new(3, [("four", 4), ("five", 5), ("nine", 9)]),
        new(4, [("three", 3), ("seven", 7), ("eight", 8)])
    ];
    private static readonly SearchValues<char> _digits = SearchValues.Create("123456789");

    public static string Solve(StreamReader reader)
    {
        var sum = 0;
        while (reader.ReadLine() is { } line)
        {
            sum += GetFirstDigitWithTokens(line) * 10 + GetLastDigitWithTokens(line);
        }
        return sum.ToString();
    }

    private static int GetFirstDigitWithTokens(in ReadOnlySpan<char> span)
    {
        for (int i = 0; i < span.Length; i++)
        {
            if (_digits.Contains(span[i]))
                return span[i] - '0';

            if (i < 2)
                continue;

            foreach (var token in _tokens)
            {
                if (i < token.Key)
                    continue;

                var currentToken = span.Slice(i - token.Key, token.Key + 1);
                var digit = CheckIfDigit(currentToken, token.Value);
                if (digit.HasValue)
                    return digit.Value;
            }
        }

        return 0;
    }
    private static int GetLastDigitWithTokens(in ReadOnlySpan<char> span)
    {
        for (int i = span.Length - 1; i >= 0; i--)
        {
            if (_digits.Contains(span[i]))
                return span[i] - '0';

            if (i > span.Length - 3)
                continue;

            foreach (var token in _tokens)
            {
                if (i + token.Key >= span.Length)
                    continue;

                var currentToken = span.Slice(i, token.Key + 1);
                var digit = CheckIfDigit(currentToken, token.Value);
                if (digit.HasValue)
                    return digit.Value;
            }
        }

        return 0;
    }

    private static int? CheckIfDigit(in ReadOnlySpan<char> span, in (string token, int value)[] tokens)
    {
        foreach (var (token, value) in tokens)
        {
            for (int i = 0; i < token.Length; i++)
            {
                if (span[i] != token[i])
                    break;

                if (i == token.Length - 1)
                    return value;
            }
        }

        return null;
    }
}
