using System.Text.RegularExpressions;

namespace AdventOfCode;

internal sealed partial class Day03_Part1 : PuzzleSolution
{
    private const string DAY = "03";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "161";

    public static string Solve(StreamReader reader)
    {
        var sum = 0;
        while (reader.ReadLine() is { } line)
        {
            foreach (var match in GetMultiplyInstructions().EnumerateMatches(line))
            {
                var span = line.AsSpan()[(match.Index)..(match.Index + match.Length)];
                var commaIndex = span.IndexOf(',');
                var a = int.Parse(span[4..commaIndex]);
                var b = int.Parse(span.Slice(commaIndex + 1, span.Length - commaIndex - 2));
                sum += a * b;
            }
        }

        return sum.ToString();
    }

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)")]
    public static partial Regex GetMultiplyInstructions();
}
