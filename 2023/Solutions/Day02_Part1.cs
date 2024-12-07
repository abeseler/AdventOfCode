namespace AdventOfCode.Solutions;

internal sealed class Day02_Part1 : PuzzleSolution
{
    private const string DAY = "02";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "8";

    private static readonly Dictionary<string, int> _maxColors = new()
    {
        { "red", 12 },
        { "blue", 14 },
        { "green", 13 }
    };

    public static string Solve(StreamReader reader)
    {
        var sum = 0;
        while (reader.ReadLine() is { } line)
        {
            var gameStart = line.IndexOf(':');
            var gameNumber = int.Parse(line.AsSpan()[5..gameStart]);
            var isPossible = IsGamePossible(line.AsSpan()[(gameStart + 2)..]);

            if (isPossible)
                sum += gameNumber;
        }
        return sum.ToString();
    }

    private static bool IsGamePossible(in ReadOnlySpan<char> lineCharacters)
    {
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
                var maxColor = _maxColors.TryGetValue(color, out var max) ? max : 0;

                if (count > maxColor)
                    return false;

                digitStart = -1;
                digitEnd = -1;
                colorStart = -1;
                colorEnd = -1;
            }
        }

        return true;
    }
}
