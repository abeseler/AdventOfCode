namespace AdventOfCode_2023.Day_2;

internal static class CubeGameResultsProcessor
{
    private static readonly Dictionary<string, int> _maxColors = new()
    {
        { "red", 12 },
        { "blue", 14 },
        { "green", 13 }
    };

    public static int GetSumOfPossibleGames(string fileName)
    {
        using var reader = new StreamReader(fileName);

        var sum = 0;
        while (reader.ReadLine() is { } line)
        {
            var gameStart = line.IndexOf(':');
            var gameNumber = int.Parse(line.AsSpan()[5..gameStart]);

            //Console.WriteLine($"Game {gameNumber}:");

            var isPossible = IsGamePossible(line.AsSpan()[(gameStart + 2)..]);
            if (isPossible)
                sum += gameNumber;

            //Console.WriteLine($"\t{(isPossible ? "Possible" : "Impossible")}");
        }
        return sum;
    }

    public static int GetSumOfMinimumCubePowers(string fileName)
    {
        using var reader = new StreamReader(fileName);

        var sum = 0;
        while (reader.ReadLine() is { } line)
        {
            var gameStart = line.IndexOf(':');
            var gameNumber = int.Parse(line.AsSpan()[5..gameStart]);

            //Console.WriteLine($"Game {gameNumber}:");

            var minimumCubePower = GetMinimumCubePower(line.AsSpan()[(gameStart + 2)..]);

            //Console.WriteLine($"\tMinimum cube power: {minimumCubePower}");

            sum += minimumCubePower;
        }
        return sum;
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
                {
                    digitStart = i;
                }
                else
                {
                    digitEnd = i;
                }
                continue;
            }
            if (char.IsLetter(currentCharacter))
            {
                if (colorStart == -1)
                {
                    colorStart = i;
                }
                else
                {
                    colorEnd = i;
                }
            }

            if (i == lineCharacters.Length - 1 || currentCharacter == ',' || currentCharacter == ';')
            {
                var color = (i == lineCharacters.Length ? lineCharacters[colorStart..] : lineCharacters[colorStart..(colorEnd + 1)]).ToString();
                var count = int.Parse(lineCharacters.Slice(digitStart, digitEnd == -1 ? 1 : digitEnd - digitStart + 1));
                var maxColor = _maxColors.TryGetValue(color, out var max) ? max : 0;
                //Console.WriteLine($"\tColor: {color}, Count: {count}, Max: {maxColor}");

                if (count > maxColor)
                {
                    return false;
                }

                digitStart = -1;
                digitEnd = -1;
                colorStart = -1;
                colorEnd = -1;
            }
        }

        return true;
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
                {
                    digitStart = i;
                }
                else
                {
                    digitEnd = i;
                }
                continue;
            }
            if (char.IsLetter(currentCharacter))
            {
                if (colorStart == -1)
                {
                    colorStart = i;
                }
                else
                {
                    colorEnd = i;
                }
            }

            if (i == lineCharacters.Length - 1 || currentCharacter == ',' || currentCharacter == ';')
            {
                var color = (i == lineCharacters.Length ? lineCharacters[colorStart..] : lineCharacters[colorStart..(colorEnd + 1)]).ToString();
                var count = int.Parse(lineCharacters.Slice(digitStart, digitEnd == -1 ? 1 : digitEnd - digitStart + 1));

                if (colorMaximums.TryGetValue(color, out var currentMaximum))
                {
                    if (count > currentMaximum)
                    {
                        colorMaximums[color] = count;
                    }
                }
                else
                {
                    colorMaximums.Add(color, count);
                }

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
            //Console.WriteLine($"\tColor: {color}, MinRequired: {count}");
        }

        return minimumCubePower;
    }
}
