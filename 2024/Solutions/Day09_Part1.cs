using System.Runtime.InteropServices;

namespace AdventOfCode.Solutions;

internal sealed class Day09_Part1 : PuzzleSolution
{
    private const string DAY = "09";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "1928";

    public static string Solve(StreamReader reader)
    {
        var input = reader.ReadLine().AsSpan();
        var emptySpace = new Queue<int>();
        var fileBlocks = new List<FileBlock>();

        var isFileBlock = true;
        var fileId = 0;
        var location = 0;
        for (var i = 0; i < input.Length; i++)
        {
            var value = input[i] - '0';
            if (isFileBlock)
            {
                while (value > 0)
                {
                    fileBlocks.Add(new FileBlock { FileId = fileId, Location = location });
                    location++;
                    value--;
                }
                fileId++;
            }
            else
            {
                while (value > 0)
                {
                    emptySpace.Enqueue(location);
                    location++;
                    value--;
                }
            }
            isFileBlock = !isFileBlock;
        }

        var checksum = CompactMemory(CollectionsMarshal.AsSpan(fileBlocks), emptySpace);

        return checksum.ToString();
    }

    private static long CompactMemory(ReadOnlySpan<FileBlock> fileBlocks, Queue<int> emptySpace)
    {
        var checksum = 0L;
        for (var i = fileBlocks.Length - 1; i >= 0; i--)
        {
            var block = fileBlocks[i];
            if (block.Location <= emptySpace.Peek())
            {
                checksum += block.Location * block.FileId;
                continue;
            }

            var emptyLocation = emptySpace.Dequeue();
            block.Location = emptyLocation;
            checksum += block.Location * block.FileId;
        }

        return checksum;
    }

    private struct FileBlock
    {
        public int FileId { get; init; }
        public int Location { get; set; }
    }
}
