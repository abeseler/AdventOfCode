using System.Runtime.InteropServices;

namespace AdventOfCode.Solutions;

internal sealed class Day09_Part2 : PuzzleSolution
{
    private const string DAY = "09";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "2858";
    public static string Solve(StreamReader reader)
    {
        var input = reader.ReadLine().AsSpan();
        var emptySpace = new List<EmptySpace>();
        var fileBlocks = new List<AoCFile>();

        var isFileBlock = true;
        var fileId = 0;
        var location = 0;
        for (var i = 0; i < input.Length; i++)
        {
            var value = input[i] - '0';
            if (isFileBlock)
            {
                fileBlocks.Add(new AoCFile { Id = fileId, Location = location, Size = value });
                location += value;
                fileId++;
            }
            else
            {
                emptySpace.Add(new EmptySpace { Location = location, Size = value });
                location += value;
            }
            isFileBlock = !isFileBlock;
        }

        var checksum = CompactMemory(CollectionsMarshal.AsSpan(fileBlocks), emptySpace);

        return checksum.ToString();
    }

    private static long CompactMemory(ReadOnlySpan<AoCFile> files, List<EmptySpace> emptySpace)
    {
        var checksum = 0L;
        for (var i = files.Length - 1; i >= 0; i--)
        {
            var file = files[i];
            var availableSpace = emptySpace.FirstOrDefault(es => es.Size >= file.Size && es.Location < file.Location);
            if (availableSpace.Size == file.Size)
            {
                file.Location = availableSpace.Location;
                emptySpace.Remove(availableSpace);
            }
            else if (availableSpace.Size > file.Size)
            {
                file.Location = availableSpace.Location;
                var newEmptySpace = new EmptySpace { Location = availableSpace.Location + file.Size, Size = availableSpace.Size - file.Size };
                emptySpace.Remove(availableSpace);
                emptySpace.Add(newEmptySpace);
            }

            for (var j = 0; j < file.Size; j++)
            {
                checksum += ((j + file.Location) * file.Id);
            }
        }
        return checksum;
    }

    private struct AoCFile
    {
        public int Id { get; init; }
        public int Size { get; init; }
        public int Location { get; set; }
    }
    private struct EmptySpace
    {
        public int Location { get; init; }
        public int Size { get; set; }
    }
}
