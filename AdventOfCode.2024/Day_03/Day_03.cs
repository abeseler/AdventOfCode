using System.Text.RegularExpressions;

namespace AdventOfCode2024;

internal static partial class Day_03
{
    public static int Part_1(string filename)
    {
        var sum = 0;
        using var reader = new StreamReader(filename);
        while (reader.ReadLine() is { } line)
        {
            var lineSpan = line.AsSpan();
            foreach (var match in GetMultiplyInstructions().EnumerateMatches(line))
            {
                var span = lineSpan[(match.Index)..(match.Index + match.Length)];
                var commaIndex = span.IndexOf(',');
                var a = int.Parse(span[4..commaIndex]);
                var b = int.Parse(span.Slice(commaIndex + 1, span.Length - commaIndex - 2));
                sum += a * b;
            }
        }

        return sum;
    }

    public static int Part_2(string filename)
    {
        var sum = 0;
        var enabled = true;
        using var reader = new StreamReader(filename);
        while (reader.ReadLine() is { } line)
        {
            foreach (var match in GetInstructions().EnumerateMatches(line))
            {
                var span = line.AsSpan()[(match.Index)..(match.Index + match.Length)];
                if (span[0] == 'd' && match.Length == 4)
                {
                    enabled = true;
                }
                else if (span[0] == 'd' && match.Length == 7)
                {
                    enabled = false;
                }
                else if (span[0] == 'm' && enabled)
                {
                    var commaIndex = span.IndexOf(',');
                    var a = int.Parse(span[4..commaIndex]);
                    var b = int.Parse(span.Slice(commaIndex + 1, span.Length - commaIndex - 2));
                    sum += a * b;
                }
            }
        }

        return sum;
    }

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)")]
    public static partial Regex GetMultiplyInstructions();

    [GeneratedRegex(@"(?:do|don't)\(\)|mul\(\d{1,3},\d{1,3}\)")]
    public static partial Regex GetInstructions();
}
