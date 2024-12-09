namespace AdventOfCode.Solutions;

internal sealed class Day09_Part1 : PuzzleSolution
{
    private const string DAY = "09";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "1928";

    private const int EMPTY_SPACE = -1;
    public static string Solve(StreamReader reader)
    {
        var memoryBlocks = new List<int>();
        var layout = reader.ReadLine().AsSpan();

        var block = 0;
        for (var i = 0; i < layout!.Length; i+=2)
        {
            var blocks = layout[i] - '0';
            var emptySpace = i < layout!.Length - 1 ? layout[i + 1] - '0' : 0;

            for (var j = 0; j < blocks; j++)
            {
                memoryBlocks.Add(block);
            }
            for (var j = 0; j < emptySpace; j++)
            {
                memoryBlocks.Add(EMPTY_SPACE);
            }

            block++;
        }

        CompactMemory(memoryBlocks);
        var checksum = CalculateChecksum(memoryBlocks);

        return checksum.ToString();
    }

    private static void CompactMemory(List<int> memoryBlocks)
    {
        for (var i = memoryBlocks.Count - 1; i >= 0; i--)
        {
            var value = memoryBlocks[i];
            if (value == EMPTY_SPACE)
            {
                continue;
            }
            memoryBlocks[i] = EMPTY_SPACE;
            var emptySpace = memoryBlocks.IndexOf(EMPTY_SPACE);
            memoryBlocks[emptySpace] = value;
        }
    }

    private static long CalculateChecksum(List<int> memoryBlocks)
    {
        long checksum = 0;
        for (var i = 0; i < memoryBlocks.Count; i++)
        {
            var value = memoryBlocks[i];
            if (value == EMPTY_SPACE)
            {
                break;
            }
            checksum += (value * i);
        }
        return checksum;
    }
}
