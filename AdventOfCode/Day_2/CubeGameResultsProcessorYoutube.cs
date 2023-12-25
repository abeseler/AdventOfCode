namespace AdventOfCode_2023.Day_2;

internal static class CubeGameResultsProcessorYoutube
{
    private const int MaxRed = 12;
    private const int MaxGreen = 13;
    private const int MaxBlue = 14;

    public static int GetSumOfPossibleGames(string fileName)
    {
        using var reader = new StreamReader(fileName);
        var runningTotal = 0;

        while (reader.ReadLine() is { } line)
        {
            var gameInfo = line.Split(':');
            var gameId = int.Parse(gameInfo[0].Split(' ')[1]);
            var rounds = gameInfo[1].Split(';', StringSplitOptions.TrimEntries);
            var isGameValid = true;

            foreach (var round in rounds)
            {                
                var colorInfos = round.Split(',', StringSplitOptions.TrimEntries);
                foreach (var color in colorInfos)
                {
                    var colorInfo = color.Split(' ');
                    var colorCount = int.Parse(colorInfo[0]);
                    var colorName = colorInfo[1];
                    switch (colorName)
                    {
                        case "red":
                            if (colorCount > MaxRed)
                            {
                                isGameValid = false;
                            }
                            break;
                        case "green":
                            if (colorCount > MaxGreen)
                            {
                                isGameValid = false;
                            }
                            break;
                        case "blue":
                            if (colorCount > MaxBlue)
                            {
                                isGameValid = false;
                            }
                            break;
                    }

                }
                if (!isGameValid)
                    break;
            }
            if (isGameValid)
                runningTotal += gameId;
        }

        return runningTotal;
    }

    public static int GetSumOfMinimumCubePowers(string fileName)
    {
        using var reader = new StreamReader(fileName);
        var runningTotal = 0;

        while (reader.ReadLine() is { } line)
        {
            var gameInfo = line.Split(':');
            var gameId = int.Parse(gameInfo[0].Split(' ')[1]);
            var rounds = gameInfo[1].Split(';', StringSplitOptions.TrimEntries);
            var maxRed = 0;
            var maxGreen = 0;
            var maxBlue = 0;

            foreach (var round in rounds)
            {
                var colorInfos = round.Split(',', StringSplitOptions.TrimEntries);
                foreach (var color in colorInfos)
                {
                    var colorInfo = color.Split(' ');
                    var colorCount = int.Parse(colorInfo[0]);
                    var colorName = colorInfo[1];
                    switch (colorName)
                    {
                        case "red":
                            maxRed = Math.Max(maxRed, colorCount);
                            break;
                        case "green":
                            maxGreen = Math.Max(maxGreen, colorCount);
                            break;
                        case "blue":
                            maxBlue = Math.Max(maxBlue, colorCount);
                            break;
                    }

                }
            }
            var product = maxRed * maxGreen * maxBlue;
            runningTotal += product;
        }

        return runningTotal;
    }
}
