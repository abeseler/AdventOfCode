namespace AdventOfCode.Solutions;

internal sealed class Day08_Part1 : PuzzleSolution
{
    private const string DAY = "08";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "6";

    public static string Solve(StreamReader reader)
    {
        var index = 0;
        var instructions = reader.ReadLine().AsSpan();
        var nodes = new Dictionary<string, (string left, string right)>();

        while (reader.ReadLine() is { } line)
        {
            if (line.Length == 0) continue;

            var key = line.Substring(0, 3);
            var left = line.Substring(7, 3);
            var right = line.Substring(12, 3);

            nodes[key] = (left, right);
        }

        var current = "AAA";
        var steps = 0;

        while (current != "ZZZ")
        {
            var (left, right) = nodes[current];
            var instruction = instructions[index];
            index = index == instructions.Length - 1 ? 0 : index + 1;

            current = instruction switch
            {
                'L' => left,
                'R' => right,
                _ => throw new InvalidOperationException()
            };

            steps++;
        }

        return steps.ToString();
    }
}
