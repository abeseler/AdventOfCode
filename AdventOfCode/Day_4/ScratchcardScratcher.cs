using System.Buffers;

namespace AdventOfCode_2023.Day_4;

internal static class ScratchcardScratcher
{
    private static SearchValues<char> _digits = SearchValues.Create("1234567890");

    public static int GetTotalPoints(string fileName)
    {
        var winningNumbers = new List<int>();
        var points = 0;

        using var reader = new StreamReader(fileName);
        while (reader.ReadLine() is { } line)
        {
            PopulateWinningNumbers(winningNumbers, line, out var cardNumbersStartIndex);
            points += GetCardPoints(winningNumbers, line, cardNumbersStartIndex);

            winningNumbers.Clear();
        }

        return points;
    }
    public static int GetCardCount(string fileName)
    {
        var winningNumbers = new List<int>();
        var cardCount = 0;

        using var reader = new StreamReader(fileName);
        while (reader.ReadLine() is { } line)
        {
            PopulateWinningNumbers(winningNumbers, line, out var cardNumbersStartIndex);

            winningNumbers.Clear();
        }

        return cardCount;
    }

    private static void PopulateWinningNumbers(List<int> winningNumbers, ReadOnlySpan<char> line, out int cardNumbersStartIndex)
    {
        var currentIndex = line.IndexOf(':') + 2;
        var digitStartIndex = 0;
        var numberOfDigits = 0;
        
        while (true)
        {
            if (line[currentIndex] == ' ')
            {
                currentIndex++;
                if (numberOfDigits > 0)
                    winningNumbers.Add(int.Parse(line.Slice(digitStartIndex, numberOfDigits)));

                numberOfDigits = 0;
            }
            if (line[currentIndex] == '|')
            {
                cardNumbersStartIndex = currentIndex + 2;
                break;
            }
            if (_digits.Contains(line[currentIndex]))
            {
                if (numberOfDigits == 0)
                    digitStartIndex = currentIndex;

                numberOfDigits++;
            }
            currentIndex++;
        }
    }
    private static int GetCardPoints(List<int> winningNumbers, ReadOnlySpan<char> line, int cardNumbersStartIndex)
    {
        var points = 0;
        var lastPointAmount = 0;
        var matches = 0;
        var currentIndex = cardNumbersStartIndex;
        var digitStartIndex = 0;
        var numberOfDigits = 0;

        while (true)
        {
            if (currentIndex >= line.Length || line[currentIndex] == ' ')
            {
                currentIndex++;
                if (numberOfDigits > 0)
                {
                    var cardNum = int.Parse(line.Slice(digitStartIndex, numberOfDigits));
                    if (winningNumbers.Contains(cardNum))
                    {
                        matches++;
                        var currentPoints = matches switch
                        {
                            1 or 2 => 1,
                            _ => lastPointAmount * 2
                        };
                        lastPointAmount = currentPoints;
                        points += currentPoints;
                    }
                }

                numberOfDigits = 0;
            }
            if (currentIndex > line.Length || line[currentIndex] == '|')
                break;

            if (_digits.Contains(line[currentIndex]))
            {
                if (numberOfDigits == 0)
                    digitStartIndex = currentIndex;

                numberOfDigits++;
            }
            currentIndex++;
        }

        return points;
    }
}
