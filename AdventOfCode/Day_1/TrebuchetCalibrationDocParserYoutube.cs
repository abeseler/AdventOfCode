namespace AdventOfCode_2023.Day_1;

internal static class TrebuchetCalibrationDocParserYoutube
{
    public static readonly Dictionary<string, int> AllDigits = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 },
        { "1", 1 },
        { "2", 2 },
        { "3", 3 },
        { "4", 4 },
        { "5", 5 },
        { "6", 6 },
        { "7", 7 },
        { "8", 8 },
        { "9", 9 },
        { "0", 0 },
    };

    public static int GetCalibrationSum(string fileName)
    {
        using var reader = new StreamReader(fileName);

        var sum = 0;
        while (reader.ReadLine() is { } line)
        {
            var firstDigit = line.First(line => char.IsDigit(line)) - '0';
            var lastDigit = line.Last(line => char.IsDigit(line)) - '0';

            var fullNumber = firstDigit * 10 + lastDigit;
            sum += fullNumber;
        }
        return sum;
    }

    public static int GetCalibrationSumIncludeTokens(string fileName)
    {
        using var reader = new StreamReader(fileName);

        var sum = 0;
        while (reader.ReadLine() is { } line)
        {
            var firstIndex = line.Length;
            var lastIndex = -1;
            var firstValue = 0;
            var lastValue = 0;

            foreach (var digit in AllDigits)
            {
                var index = line.IndexOf(digit.Key);
                if (index == -1)
                    continue;

                if (index < firstIndex)
                {
                    firstIndex = index;
                    firstValue = digit.Value;
                }

                index = line.LastIndexOf(digit.Key);

                if (index > lastIndex)
                {
                    lastIndex = index;
                    lastValue = digit.Value;
                }
            }

            var fullNumber = firstValue * 10 + lastValue;
            sum += fullNumber;
        }
        return sum;
    }
}
