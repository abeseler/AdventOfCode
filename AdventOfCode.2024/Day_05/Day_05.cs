using Rule = (int Before, int After);

namespace AdventOfCode2024;

internal static class Day_05
{
    public static int Part_1(string filename)
    {
        var sum = 0;
        var rules = new List<Rule>();

        using var reader = new StreamReader(filename);
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

            foreach (var (before, after) in rules)
            {
                var beforeIndex = Array.IndexOf(parts, before);
                var afterIndex = Array.IndexOf(parts, after);

                if (beforeIndex == -1 || afterIndex == -1) continue;

                if (beforeIndex > afterIndex)
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                sum += parts[parts.Length / 2];
            }                
        }

        return sum;
    }

    public static int Part_2(string filename)
    {
        var sum = 0;
        var rules = new List<Rule>();

        using var reader = new StreamReader(filename);
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

        return sum;
    }
}
