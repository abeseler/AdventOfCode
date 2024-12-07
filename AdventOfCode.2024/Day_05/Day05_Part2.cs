using Rule = (int before, int after);

namespace AdventOfCode2024;

internal sealed partial class Day05_Part2 : PuzzleSolution
{
    public static string Name { get; } = "Day 05 Part 2";
    public static string FileName { get; } = "Day_05/Input.txt";
    public static string TestFileName { get; } = "Day_05/Example.txt";
    public static string TestOutputExpected { get; } = "123";

    public static string Solve(StreamReader reader)
    {
        var sum = 0;
        var rules = new List<Rule>();

        while (reader.ReadLine() is { } line)
        {
            if (line.Length is 0) continue;

            if (line.IndexOf('|') is >= 0)
            {
                var seperator = line.IndexOf("|");
                var before = int.Parse(line[..seperator]);
                var after = int.Parse(line[(seperator + 1)..]);

                rules.Add((before, after));
                continue;
            }

            var parts = line.Split(",").Select(int.Parse).ToArray();
            var isMatch = true;

            var isSorted = true;
            while (true)
            {
                isSorted = true;
                foreach (var (before, after) in rules)
                {
                    var beforeIndex = Array.IndexOf(parts, before);
                    var afterIndex = Array.IndexOf(parts, after);

                    if (beforeIndex == -1 || afterIndex == -1 || beforeIndex < afterIndex) continue;

                    isSorted = false;
                    isMatch = false;
                    (parts[afterIndex], parts[beforeIndex]) = (parts[beforeIndex], parts[afterIndex]);
                    break;
                }

                if (isSorted) break;
            }

            if (isMatch is false)
            {
                sum += parts[parts.Length / 2];
            }
        }

        return sum.ToString();
    }
}
