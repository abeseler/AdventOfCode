using System.Numerics;

namespace AdventOfCode.Solutions;

internal sealed class Day08_Part1 : PuzzleSolution
{
    private const string DAY = "08";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "14";

    public static string Solve(StreamReader reader)
    {
        var antennas = new Dictionary<char, List<Antenna>>();

        var height = 0;
        var width = 0;
        while (reader.ReadLine() is { } line)
        {
            for (var col = 0; col < line.Length; col++)
            {
                var c = line[col];
                if (c == '.') continue;

                var antenna = new Antenna(new Vector2(col, height), c);
                if (antennas.ContainsKey(c) is false)
                {
                    antennas[c] = [];
                }
                antennas[c].Add(antenna);
            }
            width = line.Length;
            height++;
        }

        var antiNodes = new Dictionary<Vector2, AntiNodes>();

        foreach (var (frequency, antennaList) in antennas)
        {
            for (var i = 0; i < antennaList.Count; i++)
            {
                for (var j = i + 1; j < antennaList.Count; j++)
                {
                    var a = antennaList[i];
                    var b = antennaList[j];
                    var positions = CalculateAntiNodePositions(a, b);
                    foreach (var position in positions)
                    {
                        if (position.X < 0 || position.X >= width || position.Y < 0 || position.Y >= height)
                        {
                            continue;
                        }
                        if (antiNodes.ContainsKey(position) is false)
                        {
                            antiNodes[position] = new AntiNodes(position, []);
                        }
                        if (antiNodes[position].Frequencies.Contains(frequency) is false)
                        {
                            antiNodes[position].Frequencies.Add(frequency);
                        }
                    }
                }
            }
        }

        //Console.ForegroundColor = ConsoleColor.Gray;
        //for (var i = 0; i < height; i++)
        //{
        //    for (var j = 0; j < width; j++)
        //    {
        //        var position = new Vector2(j, i);
        //        var antenna = antennas.Values.FirstOrDefault(a => a.Any(a => a.Location == position));
        //        if (antenna is not null)
        //        {
        //            if (antiNodes.ContainsKey(position) && antiNodes[position].IsCancelledOut)
        //            {
        //                Console.BackgroundColor = ConsoleColor.DarkRed;
        //            }
        //            else if (antiNodes.ContainsKey(position))
        //            {
        //                Console.BackgroundColor = ConsoleColor.DarkBlue;
        //            }
        //            Console.Write(antenna.First(a => a.Location == position).Frequency);
        //            Console.ResetColor();
        //            continue;
        //        }

        //        if (antiNodes.ContainsKey(position) && antiNodes[position].IsCancelledOut)
        //        {
        //            Console.BackgroundColor = ConsoleColor.DarkRed;
        //            Console.Write('.');
        //            Console.ResetColor();
        //        }
        //        else if (antiNodes.ContainsKey(position))
        //        {
        //            Console.BackgroundColor = ConsoleColor.DarkBlue;
        //            Console.Write('#');
        //            Console.ResetColor();
        //        }
        //        else
        //        {
        //            Console.Write('.');
        //        }
        //    }
        //    Console.WriteLine();
        //}

        return antiNodes.Count.ToString();
    }

    private static IEnumerable<Vector2> CalculateAntiNodePositions(Antenna a, Antenna b)
    {
        var diffBA = b.Location - a.Location;
        yield return b.Location + diffBA;

        var diffAB = a.Location - b.Location;
        yield return a.Location + diffAB;
    }

    private sealed record Antenna(Vector2 Location, char Frequency)
    {

    }

    private sealed record AntiNodes(Vector2 Location, HashSet<char> Frequencies)
    {
        public bool IsCancelledOut => false;
    }
}
