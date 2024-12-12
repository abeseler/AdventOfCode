using System.Buffers;

namespace AdventOfCode.Solutions;

internal sealed class Day03_Part2 : PuzzleSolution
{
    private const string DAY = "03";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "467835";

    private static readonly SearchValues<char> _digitValues = SearchValues.Create("1234567890");

    public static string Solve(StreamReader reader)
    {
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
        sum += GetSumOfGearRatios(lineAbove, lineToCheck, string.Empty);

        return sum.ToString();
    }

    private static int GetSumOfGearRatios(ReadOnlySpan<char> lineAbove, ReadOnlySpan<char> lineToCheck, ReadOnlySpan<char> lineBelow)
    {
        var sum = 0;
        var gears = new Gears();

        for (var i = 0; i < lineToCheck.Length; i++)
        {
            if (lineToCheck[i] != '*')
                continue;

            gears.Reset();

            var spanToCheck = lineToCheck.Slice(i >= 3 ? i - 3 : i, 3); //left
            if (spanToCheck.Length > 0 && _digitValues.Contains(spanToCheck[^1]))
            {
                var start = spanToCheck.Length - 1;
                while (start > 0 && _digitValues.Contains(spanToCheck[start - 1]))
                {
                    start--;
                }
                gears.SetGear(spanToCheck[start..]);
            }

            spanToCheck = lineToCheck.Slice(i < lineToCheck.Length - 3 ? i + 1 : i, 3); //right
            if (spanToCheck.Length > 0 && _digitValues.Contains(spanToCheck[0]))
            {
                var start = 0;
                while (start < spanToCheck.Length - 1 && _digitValues.Contains(spanToCheck[start + 1]))
                {
                    start++;
                }
                gears.SetGear(spanToCheck[..(start + 1)]);
            }

            spanToCheck = lineAbove.Length > 0 ? lineAbove.Slice(i >= 3 ? i - 3 : i, 7) : []; //above
            if (spanToCheck.Length > 0)
            {
                var (gearOne, gearTwo) = GetGears(spanToCheck);

                if (gears.IsComplete && gearOne > 0)
                    break;

                gears.SetGear(gearOne);

                if (gears.IsComplete && gearTwo > 0)
                    break;

                gears.SetGear(gearTwo);
            }

            spanToCheck = lineBelow.Length > 0 ? lineBelow.Slice(i >= 3 ? i - 3 : i, 7) : []; //below
            if (spanToCheck.Length > 0)
            {
                var (gearOne, gearTwo) = GetGears(spanToCheck);

                if (gears.IsComplete && gearOne > 0)
                    break;

                gears.SetGear(gearOne);

                if (gears.IsComplete && gearTwo > 0)
                    break;

                gears.SetGear(gearTwo);
            }

            sum += gears.IsComplete ? gears.GearRatio : 0;
        }

        return sum;
    }

    private static (int, int) GetGears(ReadOnlySpan<char> chars)
    {
        (int gearOne, int gearTwo) gears = (0, 0);
        var start = 3;
        var end = 3;

        if (_digitValues.Contains(chars[3]))
        {
            while (start > 0 && _digitValues.Contains(chars[start - 1]))
            {
                start--;
            }
            while (end < chars.Length - 1 && _digitValues.Contains(chars[end + 1]))
            {
                end++;
            }
            gears.gearOne = int.Parse(start != end ? chars.Slice(start, end - start + 1) : chars.Slice(start, 1));
            return gears;
        }

        start = 2;
        var numOfDigits = 0;

        if (_digitValues.Contains(chars[start]))
        {
            numOfDigits++;
            while (start > 0 && _digitValues.Contains(chars[start - 1]))
            {
                start--;
                numOfDigits++;
            }
            gears.gearOne = int.Parse(chars.Slice(start, numOfDigits));
        }

        start = 4;
        end = 4;
        numOfDigits = 0;

        if (_digitValues.Contains(chars[start]))
        {
            numOfDigits++;
            while (end < chars.Length - 1 && _digitValues.Contains(chars[end + 1]))
            {
                end++;
                numOfDigits++;
            }
            gears.gearTwo = int.Parse(chars.Slice(start, numOfDigits));
        }

        return gears;
    }

    private struct Gears
    {
        public int GearOne { get; set; }
        public int GearTwo { get; set; }
        public bool IsComplete { get; set; }

        public readonly int GearRatio => GearOne * GearTwo;
        public void SetGear(int gear)
        {
            if (gear == 0)
                return;

            if (GearOne == 0)
            {
                GearOne = gear;
                return;
            }
            GearTwo = gear;
            IsComplete = true;
        }
        public void SetGear(ReadOnlySpan<char> chars)
        {
            SetGear(int.Parse(chars));
        }
        public void Reset()
        {
            GearOne = 0;
            GearTwo = 0;
            IsComplete = false;
        }
    }
}
