using System.Drawing;

namespace AdventOfCode.Solutions;

internal sealed class Day10_Part2 : PuzzleSolution
{
    private const string DAY = "10";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample2";
    public static string TestOutputExpected { get; } = "4";

    public static string Solve(StreamReader reader)
    {
        var width = 0;
        var height = 0;
        var map = new Dictionary<Point, Pipe>();
        var startingPoint = new Point(0, 0);
        while (reader.ReadLine() is { } line)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] is '.') continue;
                if (line[i] is 'S')
                {
                    startingPoint = new Point(i, height);
                    continue;
                }

                var node = new Pipe(new Point(i, height), (PipeType)line[i]);
                map[node.Position] = node;
            }

            width = line.Length;
            height++;
        }

        var start = new Pipe(startingPoint, CalculateStartType(startingPoint, map));
        map.Add(start.Position, start);

        var primaryLoop = CalculateLoop(start!, map);

        Console.ResetColor();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var point = new Point(x, y);
                if (map.TryGetValue(point, out var pipe) is false)
                {
                    Console.Write('.');
                    continue;
                }

                if (primaryLoop.Contains(pipe))
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                }
                Console.Write(pipe.Display);
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        return "";
    }

    private static PipeType CalculateStartType(Point start, Dictionary<Point, Pipe> map)
    {
        PipeType? n = map.TryGetValue(start.North(), out var north) ? north.Type : null;
        PipeType? s = map.TryGetValue(start.South(), out var south) ? south.Type : null;
        PipeType? w = map.TryGetValue(start.West(), out var west) ? west.Type : null;
        PipeType? e = map.TryGetValue(start.East(), out var east) ? east.Type : null;

        var connectedNorth = n is PipeType.Vertical or PipeType.SE_Bend or PipeType.SW_Bend;
        var connectedSouth = s is PipeType.Vertical or PipeType.NE_Bend or PipeType.NW_Bend;
        var connectedWest = w is PipeType.Horizontal or PipeType.NE_Bend or PipeType.SE_Bend;
        var connectedEast = e is PipeType.Horizontal or PipeType.NW_Bend or PipeType.SW_Bend;

        var type = (connectedNorth, connectedSouth, connectedEast, connectedWest) switch
        {
            (true, true, false, false) => PipeType.Vertical,
            (false, false, true, true) => PipeType.Horizontal,
            (true, false, true, false) => PipeType.NE_Bend,
            (true, false, false, true) => PipeType.NW_Bend,
            (false, true, true, false) => PipeType.SE_Bend,
            (false, true, false, true) => PipeType.SW_Bend,
            _ => throw new InvalidOperationException("Invalid starting point"),
        };

        return type;
    }

    private static HashSet<Pipe> CalculateLoop(Pipe start, Dictionary<Point, Pipe> map)
    {
        var primaryLoop = new HashSet<Pipe>
        {
            start!
        };

        var queue = new Queue<Pipe>();
        queue.Enqueue(start!);

        while (queue.TryDequeue(out var current))
        {
            primaryLoop.Add(current);
            foreach (var node in current.Connections(map))
            {
                if (primaryLoop.Contains(node)) continue;
                queue.Enqueue(node);
            }

        }

        return primaryLoop;
    }

    private sealed record Pipe(Point Position, PipeType Type)
    {
        public char Display => Type switch
        {
            PipeType.NE_Bend => '└',
            PipeType.NW_Bend => '┘',
            PipeType.SW_Bend => '┐',
            PipeType.SE_Bend => '┌',
            _ => (char)Type
        };

        public bool IsConnectedTo(Pipe other)
        {
            return Type switch
            {
                PipeType.Vertical => other.Position == Position.North() || other.Position == Position.South(),
                PipeType.Horizontal => other.Position == Position.East() || other.Position == Position.West(),
                PipeType.NE_Bend => other.Position == Position.North() || other.Position == Position.East(),
                PipeType.NW_Bend => other.Position == Position.North() || other.Position == Position.West(),
                PipeType.SW_Bend => other.Position == Position.South() || other.Position == Position.West(),
                PipeType.SE_Bend => other.Position == Position.South() || other.Position == Position.East(),
                _ => false
            };
        }

        public IEnumerable<Pipe> Connections(Dictionary<Point, Pipe> map)
        {
            if (Type is PipeType.Vertical or PipeType.NE_Bend or PipeType.NW_Bend
                && map.TryGetValue(Position.North(), out var node)
                && IsConnectedTo(node)
                && node.IsConnectedTo(this))
            {
                yield return node;
            }
            if (Type is PipeType.Vertical or PipeType.SE_Bend or PipeType.SW_Bend
                && map.TryGetValue(Position.South(), out node)
                && IsConnectedTo(node)
                && node.IsConnectedTo(this))
            {
                yield return node;
            }
            if (Type is PipeType.Horizontal or PipeType.SW_Bend or PipeType.NW_Bend
                && map.TryGetValue(Position.West(), out node)
                && IsConnectedTo(node)
                && node.IsConnectedTo(this))
            {
                yield return node;
            }
            if (Type is PipeType.Horizontal or PipeType.NE_Bend or PipeType.SE_Bend
                && map.TryGetValue(Position.East(), out node)
                && IsConnectedTo(node)
                && node.IsConnectedTo(this))
            {
                yield return node;
            }
        }
    }

    private enum PipeType
    {
        Vertical = '|',
        Horizontal = '-',
        NE_Bend = 'L',
        NW_Bend = 'J',
        SW_Bend = '7',
        SE_Bend = 'F',
    }

    private sealed class PipeSegment
    {
        public Point Start { get; }
        public Point End { get; }
        public PipeSegment(Point start, Point end)
        {
            Start = start;
            End = end;
        }
        public bool Intersects(PipeSegment other)
        {
            var a = End.Y - Start.Y;
            var b = Start.X - End.X;
            var c = a * Start.X + b * Start.Y;
            var u = Math.Abs(a * other.Start.X + b * other.Start.Y - c);
            var v = Math.Abs(a * other.End.X + b * other.End.Y - c);
            return u * v < 0;
        }
    }
}

file static class PointExt
{
    public static Point North(this Point point) => new(point.X, point.Y - 1);
    public static Point South(this Point point) => new(point.X, point.Y + 1);
    public static Point West(this Point point) => new(point.X - 1, point.Y);
    public static Point East(this Point point) => new(point.X + 1, point.Y);
}
