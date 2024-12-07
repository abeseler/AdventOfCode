using System.Buffers;

namespace AdventOfCode.Solutions;

internal sealed class Day04_Part1 : PuzzleSolution
{
    private const string DAY = "04";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "13";

    private static readonly SearchValues<char> _digits = SearchValues.Create("1234567890");

    public static string Solve(StreamReader reader)
    {
        var winningNumbers = new List<int>();
        var points = 0;

        while (reader.ReadLine() is { } line)
        {
            PopulateWinningNumbers(winningNumbers, line, out var cardNumbersStartIndex);
            points += GetCardPoints(winningNumbers, line, cardNumbersStartIndex);

            winningNumbers.Clear();
        }

        return points.ToString();
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
