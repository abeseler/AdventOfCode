namespace AdventOfCode.Solutions;

internal sealed class Day08_Part2 : PuzzleSolution
{
    private const string DAY = "08";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample2";
    public static string TestOutputExpected { get; } = "6";

    public static string Solve(StreamReader reader)
    {
        var instructions = reader.ReadLine().AsSpan();
        var map = new Dictionary<string, (string left, string right)>();
        var nodes = new List<string>();

        while (reader.ReadLine() is { } line)
        {
            if (line.Length == 0) continue;

            var key = line.Substring(0, 3);
            var left = line.Substring(7, 3);
            var right = line.Substring(12, 3);

            if (key[2] == 'A') nodes.Add(key);

            map[key] = (left, right);
        }

        var stepCounts = new long[nodes.Count];

        var steps = 0;

        while (stepCounts.Any(s => s == 0))
        {
            var instruction = instructions[steps++ % instructions.Length];
            for (int i = 0; i < nodes.Count; i++)
            {
                if (stepCounts[i] > 0) { continue; }
                var next = instruction == 'L' ? map[nodes[i]].left : map[nodes[i]].right;
                nodes[i] = next;
                if (next[2] == 'Z')
                {
                    stepCounts[i] = steps;
                }
            }
        }

        var result = MinSharedMultiple(stepCounts);

        return result.ToString();
    }

    public static long MinSharedMultiple(long[] numbers)
    {
        return numbers.Aggregate((l, r) =>
        {
            return Math.Abs(l * r) / MaxSharedDivisor(l, r);
        });
    }
    public static long MaxSharedDivisor(long l, long r)
    {
        if (r == 0) return l;

        return MaxSharedDivisor(r, l % r);
    }
}
