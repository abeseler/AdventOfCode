namespace AdventOfCode.Solutions;

/// <summary>
/// https://adventofcode.com/2024/day/11
/// </summary>
internal sealed class Day11_Part1 : PuzzleSolution
{
    private const string DAY = "11";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "55312";
    public static string Solve(StreamReader reader)
    {
        var stones = new Dictionary<long, long>();
        foreach (var stone in reader.ReadToEnd().Split(' ').Select(long.Parse))
        {
            stones.Add(stone, 1);
        }

        for (var i = 0; i < 25; i++)
        {
            stones = Blink(stones);
        }

        return stones.Values.Sum().ToString();
    }

    private static Dictionary<long, long> Blink(Dictionary<long, long> stones)
    {
        var newStones = new Dictionary<long, long>();
        foreach (var (stone, count) in stones)
        {
            var numOfDigits = stone switch
            {
                < 10 => 1,
                < 100 => 2,
                < 1000 => 3,
                < 10000 => 4,
                < 100000 => 5,
                < 1000000 => 6,
                < 10000000 => 7,
                < 100000000 => 8,
                < 1000000000 => 9,
                < 10000000000 => 10,
                < 100000000000 => 11,
                < 1000000000000 => 12,
                < 10000000000000 => 13,
                < 100000000000000 => 14,
                < 1000000000000000 => 15,
                < 10000000000000000 => 16,
                < 100000000000000000 => 17,
                < 1000000000000000000 => 18,
                _ => 19
            };

            if (stone is 0)
            {
                if (newStones.ContainsKey(1))
                {
                    newStones[1] += count;
                }
                else
                {
                    newStones[1] = count;
                }
            }
            else if (numOfDigits % 2 == 0)
            {
                var left = stone / (long)Math.Pow(10, numOfDigits / 2);
                var right = stone % (long)Math.Pow(10, numOfDigits / 2);
                if (newStones.ContainsKey(left))
                {
                    newStones[left] += count;
                }
                else
                {
                    newStones[left] = count;
                }
                if (newStones.ContainsKey(right))
                {
                    newStones[right] += count;
                }
                else
                {
                    newStones[right] = count;
                }
            }
            else
            {
                var newStone = stone * 2024;
                if (newStones.ContainsKey(newStone))
                {
                    newStones[newStone] += count;
                }
                else
                {
                    newStones[newStone] = count;
                }
            }
        }
        return newStones;
    }
}
