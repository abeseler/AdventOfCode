using System.Diagnostics;

namespace AdventOfCode.Solutions;

/// <summary>
/// https://adventofcode.com/2024/day/7
/// </summary>
internal sealed class Day07_Part1 : PuzzleSolution
{
    private const string DAY = "07";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "3749";

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

            for (int i = 0; i < (1 << operators.Length); i++)
            {
                for (int j = 0; j < operators.Length; j++)
                {
                    operators[j] = (i & (1 << j)) != 0 ? 1 : 0;
                }

                var current = operands[0];
                for (int j = 1; j < operands.Length; j++)
                {
                    current = operators[j - 1] switch
                    {
                        0 => current + operands[j],
                        1 => current * operands[j],
                        _ => throw new UnreachableException()
                    };
                }
                if (current == result)
                {
                    sum += result;
                    break;
                }
            }
        }

        return sum.ToString();
    }
}
