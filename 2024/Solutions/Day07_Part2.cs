using System.Diagnostics;

namespace AdventOfCode.Solutions;

/// <summary>
/// https://adventofcode.com/2024/day/7#part2
/// </summary>
internal sealed class Day07_Part2 : PuzzleSolution
{
    private const string DAY = "07";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "11387";

    public static string Solve(StreamReader reader)
    {
        var operations = new List<(long result, long[] operands)>();
        while (reader.ReadLine() is { } line)
        {
            var resultEnd = line.IndexOf(':');
            var result = long.Parse(line[..resultEnd]);
            var operands = line[(resultEnd + 2)..].Split(' ').Select(long.Parse).ToArray();
            operations.Add((result, operands));
        }

        var sum = 0L;
        int[] operators = [];

        foreach (var (result, operands) in operations)
        {
            operators = new int[operands.Length - 1];
            var value = GenerateAndTestCombinations(result, operands, operators, 0);
            sum += value;
        }

        return sum.ToString();
    }

    private static long GenerateAndTestCombinations(long result, long[] operands, int[] operators, int position)
    {
        if (position == operators.Length)
        {
            if (CalculatesToExpected(result, operands, operators))
            {
                return result;
            }
            return 0;
        }

        for (int i = 0; i < 3; i++)
        {
            operators[position] = i;
            var value = GenerateAndTestCombinations(result, operands, operators, position + 1);
            if (value != 0)
            {
                return value;
            }
        }

        return 0;
    }

    private static bool CalculatesToExpected(long result, long[] operands, int[] operators)
    {
        var value = operands[0];

        for (int i = 1; i < operands.Length; i++)
        {
            var right = operands[i];
            value = operators[i - 1] switch
            {
                0 => value + right,
                1 => value * right,
                2 => right + right switch
                {
                    < 10 => value * 10,
                    < 100 => value * 100,
                    < 1_000 => value * 1_000,
                    < 10_000 => value * 10_000,
                    _ => throw new UnreachableException()
                },
                _ => throw new UnreachableException()
            };

            if (value > result)
            {
                return false;
            }
        }

        return value == result;
    }
}
