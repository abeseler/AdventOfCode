using System.Buffers;
using System.Runtime.InteropServices;

namespace AdventOfCode.Solutions;

internal sealed class Day04_Part2 : PuzzleSolution
{
    private const string DAY = "04";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "30";

    private static readonly SearchValues<char> _digits = SearchValues.Create("1234567890");

    public static string Solve(StreamReader reader)
    {
        var cards = new List<Card>();
        var winningNumbers = new List<int>();
        var cardCount = 0;

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

        return cardCount.ToString();
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
