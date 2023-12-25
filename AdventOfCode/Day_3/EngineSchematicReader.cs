using System.Buffers;

namespace AdventOfCode_2023.Day_3;

internal static class EngineSchematicReader
{
    private static readonly SearchValues<char> _searchValues = SearchValues.Create("1234567890.");
    private static readonly SearchValues<char> _digitValues = SearchValues.Create("1234567890");

    public static int GetSumOfPartNumbers(string fileName)
    {
        using var reader = new StreamReader(fileName);

        var lineAbove = string.Empty;
        var lineToCheck = string.Empty;

        var sum = 0;
        while (reader.ReadLine() is { } line)
        {
            if (lineToCheck.Length == 0)
            {
                lineToCheck = line;
                continue;
            }

            sum += GetSumOfPartNumbers(lineAbove, lineToCheck, line);

            lineAbove = lineToCheck;
            lineToCheck = line;
        }
        sum = GetSumOfPartNumbers(lineAbove, lineToCheck, string.Empty);

        return sum;
    }
    public static int GetSumOfGearRatios(string fileName)
    {
        using var reader = new StreamReader(fileName);

        var lineAbove = string.Empty;
        var lineToCheck = string.Empty;

        var sum = 0;
        while (reader.ReadLine() is { } line)
        {
            if (lineToCheck.Length == 0)
            {
                lineToCheck = line;
                continue;
            }

            sum += GetSumOfGearRatios(lineAbove, lineToCheck, line);

            lineAbove = lineToCheck;
            lineToCheck = line;
        }
        sum = GetSumOfGearRatios(lineAbove, lineToCheck, string.Empty);

        return sum;
    }
    private static int GetSumOfPartNumbers(ReadOnlySpan<char> lineAbove, ReadOnlySpan<char> lineToCheck, ReadOnlySpan<char> lineBelow)
    {
        var sum = 0;
        var partNumberStart = -1;
        var partNumberEnd = -1;

        for (var i = 0; i < lineToCheck.Length; i++)
        {
            var currentChar = lineToCheck[i];
            var isDigit = char.IsDigit(currentChar);
            if (isDigit)
            {
                if (partNumberStart == -1)
                {
                    partNumberStart = i;
                    partNumberEnd = i;

                    if (i != lineToCheck.Length - 1)
                        continue;
                }
                partNumberEnd = i;
            }
            if (partNumberStart != -1 && (!isDigit || i == lineToCheck.Length - 1))
            {
                int? partNumberValue = null;
                var partNumber = lineToCheck[partNumberStart..(partNumberEnd + 1)];
                //Console.WriteLine($"\tFound possible part number: {partNumber}");
                if (partNumberStart > 0)
                {
                    if (!_searchValues.Contains(lineToCheck[partNumberStart - 1]))
                    {
                        partNumberValue = int.Parse(partNumber);
                    }
                }
                if (partNumberValue is null && partNumberEnd < lineToCheck.Length - 1)
                {
                    if (!_searchValues.Contains(lineToCheck[partNumberEnd + 1]))
                    {
                        partNumberValue = int.Parse(partNumber);
                    }
                }
                if (partNumberValue is null && lineAbove.Length > 0)
                {
                    var aboveIndexStart = partNumberStart > 0 ? partNumberStart - 1 : 0;
                    var aboveIndexEnd = partNumberEnd < lineAbove.Length - 1 ? partNumberEnd + 1 : lineAbove.Length - 1;
                    var aboveSearchSpan = lineAbove[aboveIndexStart..(aboveIndexEnd + 1)];
                    var containsSearchValue = aboveSearchSpan.ContainsAnyExcept(_searchValues);
                    if (containsSearchValue)
                        partNumberValue = int.Parse(partNumber);
                }
                if (partNumberValue is null && lineBelow.Length > 0)
                {
                    var belowIndexStart = partNumberStart > 0 ? partNumberStart - 1 : 0;
                    var belowIndexEnd = partNumberEnd < lineBelow.Length - 1 ? partNumberEnd + 1 : lineBelow.Length - 1;
                    var belowSearchSpan = lineBelow[belowIndexStart..(belowIndexEnd + 1)];
                    var containsSearchValue = belowSearchSpan.ContainsAnyExcept(_searchValues);
                    if (containsSearchValue)
                        partNumberValue = int.Parse(partNumber);
                }
                if (partNumberValue is not null)
                {
                    //Console.WriteLine($"\tFound part number: {partNumberValue}");
                    sum += partNumberValue.Value;
                }
                partNumberStart = -1;
                partNumberEnd = -1;
            }
        }

        return sum;
    }
    private static int GetSumOfGearRatios(ReadOnlySpan<char> lineAbove, ReadOnlySpan<char> lineToCheck, ReadOnlySpan<char> lineBelow)
    {
        var sum = 0;

        for (var i = 0; i < lineToCheck.Length; i++)
        {
            var currentChar = lineToCheck[i];
            if (currentChar != '*')
                continue;


        }

        return sum;
    }
}
