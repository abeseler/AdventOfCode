using System.Runtime.InteropServices;

namespace AdventOfCode.Solutions;

internal sealed class Day09_Part2 : PuzzleSolution
{
    private const string DAY = "09";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "2858";

    private const int EMPTY_SPACE = -1;
    public static string Solve(StreamReader reader)
    {
        var memory = new List<int>();
        var layout = reader.ReadLine().AsSpan();

        var block = 0;
        for (var i = 0; i < layout!.Length; i += 2)
        {
            var blocks = layout[i] - '0';
            var emptySpace = i < layout!.Length - 1 ? layout[i + 1] - '0' : 0;

            for (var j = 0; j < blocks; j++)
            {
                memory.Add(block);
            }
            for (var j = 0; j < emptySpace; j++)
            {
                memory.Add(EMPTY_SPACE);
            }

            block++;
        }

        var memorySpan = CollectionsMarshal.AsSpan(memory);

        CompactMemory(memorySpan);
        var checksum = CalculateChecksum(memorySpan);

        return checksum.ToString();
    }

    private static void CompactMemory(Span<int> memoryBlocks)
    {
        var pageEnd = 0;
        for (var i = memoryBlocks.Length - 1; i >= 0; i--)
        {
            var value = memoryBlocks[i];
            if (value == 0) break;
            if (value == EMPTY_SPACE) continue;

            pageEnd = i;
            while (true)
            {
                if (memoryBlocks[i - 1] == value)
                {
                    i--;
                }
                else
                {
                    break;
                }
            }

            var pageStart = i;
            var pageLength = pageEnd - pageStart + 1;

            var space = FindSpaceForPage(memoryBlocks[..pageStart], pageLength);
            if (space != -1)
            {
                for (var j = pageStart; j <= pageEnd; j++)
                {
                    memoryBlocks[space++] = value;
                    memoryBlocks[j] = EMPTY_SPACE;
                }
            }
        }
    }

    private static int FindSpaceForPage(Span<int> memoryBlocks, int pageLength)
    {
        for (var i = 0; i < memoryBlocks.Length; i++)
        {
            if (memoryBlocks[i] != EMPTY_SPACE) continue;

            var space = 1;
            while (space < pageLength)
            {
                var next = i + space;
                if (next >= memoryBlocks.Length) return -1;
                if (memoryBlocks[next] != EMPTY_SPACE) break;

                space++;
            }
            if (space == pageLength)
            {
                return i;
            }
            else
            {
                i += space;
            }
        }
        return -1;
    }

    private static long CalculateChecksum(Span<int> memoryBlocks)
    {
        long checksum = 0;
        for (var i = 0; i < memoryBlocks.Length; i++)
        {
            var value = memoryBlocks[i];
            if (value == EMPTY_SPACE)
            {
                continue;
            }
            checksum += (value * i);
        }
        return checksum;
    }
}
