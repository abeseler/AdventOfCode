namespace AdventOfCode.Solutions;

internal sealed class Day02_Part2 : PuzzleSolution
{
    private const string DAY = "02";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "2286";

    public static string Solve(StreamReader reader)
    {
        var sum = 0;
        while (reader.ReadLine() is { } line)
        {
            var gameStart = line.IndexOf(':');
            var gameNumber = int.Parse(line.AsSpan()[5..gameStart]);
            var minimumCubePower = GetMinimumCubePower(line.AsSpan()[(gameStart + 2)..]);

            sum += minimumCubePower;
        }
        return sum.ToString();
    }

    private static int GetMinimumCubePower(in ReadOnlySpan<char> lineCharacters)
    {
        var colorMaximums = new Dictionary<string, int>();

        var digitStart = -1;
        var digitEnd = -1;
        var colorStart = -1;
        var colorEnd = -1;

        for (var i = 0; i < lineCharacters.Length; i++)
        {
            var currentCharacter = lineCharacters[i];
            if (char.IsDigit(currentCharacter))
            {
                if (digitStart == -1)
                    digitStart = i;
                else
                    digitEnd = i;

                continue;
            }
            if (char.IsLetter(currentCharacter))
            {
                if (colorStart == -1)
                    colorStart = i;
                else
                    colorEnd = i;
            }

            if (i == lineCharacters.Length - 1 || currentCharacter == ',' || currentCharacter == ';')
            {
                var color = (i == lineCharacters.Length ? lineCharacters[colorStart..] : lineCharacters[colorStart..(colorEnd + 1)]).ToString();
                var count = int.Parse(lineCharacters.Slice(digitStart, digitEnd == -1 ? 1 : digitEnd - digitStart + 1));

                if (colorMaximums.TryGetValue(color, out var currentMaximum))
                {
                    if (count > currentMaximum)
                        colorMaximums[color] = count;
                }
                else
                    colorMaximums.Add(color, count);

                digitStart = -1;
                digitEnd = -1;
                colorStart = -1;
                colorEnd = -1;
            }
        }

        var minimumCubePower = 1;

        foreach (var (color, count) in colorMaximums)
        {
            minimumCubePower *= count;
        }

        return minimumCubePower;
    }
}
