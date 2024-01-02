using System.Buffers;
using System.Runtime.InteropServices;

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
        var cards = new List<Card>();
        var winningNumbers = new List<int>();
        var cardCount = 0;

        using var reader = new StreamReader(fileName);
        while (reader.ReadLine() is { } line)
        {
            cardCount++;
            PopulateWinningNumbers(winningNumbers, line, out var cardNumbersStartIndex);
            var numberOfMatches = GetCardMatchCount(winningNumbers, line, cardNumbersStartIndex);
            cards.Add(new()
            {
                CardNumber = cardCount,
                Matches = numberOfMatches,
                Copies = 0
            });

            winningNumbers.Clear();
        }

        var cardsAsSpan = CollectionsMarshal.AsSpan(cards);
        foreach (var card in cardsAsSpan)
        {
            cardCount += card.Copies;
            var nextCard = card.CardNumber;
            for (var j = 0; j < card.Matches; j++)
            {
                cardsAsSpan[nextCard].Copies += card.Copies + 1;
                nextCard++;
            }
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
    private static int GetCardMatchCount(List<int> winningNumbers, ReadOnlySpan<char> line, int cardNumbersStartIndex)
    {
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
                        matches++;
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

        return matches;
    }

    private struct Card
    {
        public int CardNumber { get; set; }
        public int Matches { get; set; }
        public int Copies { get; set; }
    }
}
