namespace AdventOfCode_2023.Day_4;

internal static class ScratchcardScratcherYoutube
{
    public static int GetTotalPoints(string fileName)
    {
        int runningTotal = 0;

        var input = InputTools.ReadAllLines(fileName);
        foreach (var line in input)
        {
            var parts = line.Split(':');
            var numbers = parts[1].Split('|');
            var pickedNumbers = numbers[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray();
            var ourNumbers = numbers[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray();

            var matchCount = pickedNumbers.Intersect(ourNumbers).Count();
            if (matchCount == 0)
            {
                continue;
            }

            runningTotal += (1 << (matchCount - 1));
        }

        return runningTotal;
    }

    public static int GetCardCount(string fileName)
    {
        var input = InputTools.ReadAllLines(fileName);
        int[] cardCount = new int[input.Length];
        for (int i = 0; i < cardCount.Length; i++)
        {
            cardCount[i] = 1;
        }

        for (int cardId = 0; cardId < input.Length; cardId++)
        {
            string? line = input[cardId];
            var parts = line.Split(':');
            var numbers = parts[1].Split('|');
            var pickedNumbers = numbers[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray();
            var ourNumbers = numbers[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray();

            var matchCount = pickedNumbers.Intersect(ourNumbers).Count();

            for (int i = 0; i < matchCount; i++)
            {
                cardCount[cardId + 1 + i] += cardCount[cardId];
            }
        }

        return cardCount.Sum();
    }

    private static class InputTools
    {
        public static string[] ReadAllLines(string fileName)
        {
            var lines = new List<string>();
            using var reader = new StreamReader(fileName);
            while (reader.ReadLine() is { } line)
            {
                lines.Add(line);
            }
            return lines.ToArray();
        }
    }
}
