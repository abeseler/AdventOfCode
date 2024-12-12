using System.Drawing;

namespace AdventOfCode.Solutions;

internal sealed class Day10_Part1 : PuzzleSolution
{
    private const string DAY = "10";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "8";

    public static string Solve(StreamReader reader)
    {
        var rows = 0;
        var nodes = new Dictionary<Point, Node>();
        Node? startingNode = null;
        while (reader.ReadLine() is { } line)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '.') continue;

                var position = new Point(i, rows);
                var node = new Node(position, line[i]);
                nodes[position] = node;

                if (node.Value == 'S')
                {
                    startingNode = node;
                }
            }

            rows++;
        }
        
        var visited = new Dictionary<Node, int>();
        var queue = new Queue<(Node, int)>();
        queue.Enqueue((startingNode!, 0));

        while (queue.Count > 0)
        {
            var (node, distance) = queue.Dequeue();
            if (visited.ContainsKey(node))
            {
                continue;
            }
            visited[node] = distance;
            foreach (var connection in node.Connections(nodes))
            {
                queue.Enqueue((connection, distance + 1));
            }
        }

        return visited.Values.Max().ToString();
    }

    private sealed class Node(Point position, char value)
    {
        public char Value { get; private init; } = value;
        public Point Position { get; private init; } = position;

        public bool IsConnectedTo(Node other)
        {
            return Value switch
            {
                '|' => other.Position == Position.North() || other.Position == Position.South(),
                '-' => other.Position == Position.East() || other.Position== Position.West(),
                'L' => other.Position == Position.North() || other.Position == Position.East(),
                'J' => other.Position == Position.North() || other.Position == Position.West(),
                '7' => other.Position == Position.South() || other.Position == Position.West(),
                'F' => other.Position == Position.South() || other.Position == Position.East(),
                'S' => other.Position == Position.North() || other.Position == Position.South() || other.Position == Position.West() || other.Position == Position.East(),
                _ => false
            };
        }

        public IEnumerable<Node> Connections(Dictionary<Point, Node> nodes)
        {
            if (Value is 'S' or '|' or 'L' or 'J'
                && nodes.TryGetValue(Position.North(), out var node)
                && IsConnectedTo(node)
                && node.IsConnectedTo(this))
            {
                yield return node;
            }
            if (Value is 'S' or '|' or 'F' or '7'
                && nodes.TryGetValue(Position.South(), out node)
                && IsConnectedTo(node)
                && node.IsConnectedTo(this))
            {
                yield return node;
            }
            if (Value is 'S' or '-' or '7' or 'J'
                && nodes.TryGetValue(Position.West(), out node)
                && IsConnectedTo(node)
                && node.IsConnectedTo(this))
            {
                yield return node;
            }
            if (Value is 'S' or '-' or 'L' or 'F'
                && nodes.TryGetValue(Position.East(), out node)
                && IsConnectedTo(node)
                && node.IsConnectedTo(this))
            {
                yield return node;
            }
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
