namespace AdventOfCode_2023.Day_3;

internal static class EngineSchematicReaderYoutube
{
    public static int GetSumOfPartNumbers(string fileName)
    {
        var input = InputTools.ReadAllLines(fileName);

        var width = input[0].Length;
        var height = input.Length;

        var map = new char[width, height];
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                map[x, y] = input[y][x];
            }
        }

        var runningTotal = 0;
        var currentNumber = 0;
        var hasNeighboringSymbol = false;

        for (var y = 0; y < height; y++)
        {
            void EndCurrentNumber()
            {
                if (currentNumber != 0 && hasNeighboringSymbol)
                {
                    runningTotal += currentNumber;
                }
                currentNumber = 0;
                hasNeighboringSymbol = false;
            }

            for (var x = 0; x < height; x++)
            {
                var character = map[x, y];
                if (char.IsDigit(character))
                {
                    var value = character - '0';
                    currentNumber = currentNumber * 10 + value;
                    foreach (var direction in Directions.WithDiagonals)
                    {
                        var neigbhorX = x + direction.X;
                        var neigbhorY = y + direction.Y;
                        if (neigbhorX < 0 || neigbhorX >= width || neigbhorY < 0 || neigbhorY >= height)
                        {
                            continue;
                        }

                        var neighborCharacter = map[neigbhorX, neigbhorY];
                        if (!char.IsDigit(neighborCharacter) && neighborCharacter != '.')
                        {
                            hasNeighboringSymbol = true;
                        }
                    }
                }
                else
                {
                    EndCurrentNumber();
                }
            }

            EndCurrentNumber();
        }

        return runningTotal;
    }
    public static int GetSumOfGearRatios(string fileName)
    {
        var input = InputTools.ReadAllLines(fileName);

        var width = input[0].Length;
        var height = input.Length;

        var map = new char[width, height];
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                map[x, y] = input[y][x];
            }
        }

        var runningTotal = 0;
        var currentNumber = 0;
        var asterisks = new Dictionary<Point, List<int>>();
        var neighboringAsterisks = new HashSet<Point>();

        for (var y = 0; y < height; y++)
        {
            void EndCurrentNumber()
            {
                if (currentNumber != 0 && neighboringAsterisks.Count > 0)
                {
                    foreach (var neighboringAsterisk in neighboringAsterisks)
                    {
                        var x = neighboringAsterisk.X;
                        var y = neighboringAsterisk.Y;
                        if (!asterisks.ContainsKey((x, y)))
                        {
                            asterisks[(x, y)] = [];
                        }

                        asterisks[(x, y)].Add(currentNumber);
                    }
                }
                currentNumber = 0;
                neighboringAsterisks.Clear();
            }

            for (var x = 0; x < height; x++)
            {
                var character = map[x, y];
                if (char.IsDigit(character))
                {
                    var value = character - '0';
                    currentNumber = currentNumber * 10 + value;
                    foreach (var direction in Directions.WithDiagonals)
                    {
                        var neigbhorX = x + direction.X;
                        var neigbhorY = y + direction.Y;
                        if (neigbhorX < 0 || neigbhorX >= width || neigbhorY < 0 || neigbhorY >= height)
                        {
                            continue;
                        }

                        var neighborCharacter = map[neigbhorX, neigbhorY];
                        if (neighborCharacter == '*')
                        {
                            neighboringAsterisks.Add((neigbhorX, neigbhorY));
                        }
                    }
                }
                else
                {
                    EndCurrentNumber();
                }
            }

            EndCurrentNumber();
        }

        foreach (var (point, numbers) in asterisks)
        {
            if (numbers.Count == 2)
            {
                runningTotal += numbers[0] * numbers[1];
            }
        }

        return runningTotal;
    }

    private static class InputTools
    {
        public static string[] ReadAllLines(string fileName)
        {
            var lines = new List<string>();
            using var reader = new StreamReader(fileName);
            while (reader.ReadLine() is { } line)
            {
                lines.Add(line);
            }
            return lines.ToArray();
        }
    }

    private static class Directions
    {
        public static Point[] WithoutDiagonals { get; } =
        [
            new Point(0, -1),
            new Point(1, 0),
            new Point(0, 1),
            new Point(-1, 0)
        ];

        public static Point[] WithDiagonals { get; } =
        [
            new Point(0, 1),
            new Point(1, 0),
            new Point(0, -1),
            new Point(-1, 0),
            new Point(1, 1),
            new Point(-1, 1),
            new Point(1, -1),
            new Point(-1, -1)
        ];
    }

    public record struct Point(int X, int Y)
    {
        public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);

        public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);

        public Point Normalize() => new Point(X != 0 ? X / Math.Abs(X) : 0, Y != 0 ? Y / Math.Abs(Y) : 0);

        public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);

        public int ManhattanDistance(Point b) => Math.Abs(X - b.X) + Math.Abs(Y - b.Y);
    }
}
