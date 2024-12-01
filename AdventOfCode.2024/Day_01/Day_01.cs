namespace AdventOfCode2024;

internal static class Day_01
{
    public static int Part_1(string filename)
    {
        var leftValues = new List<int>();
        var rightValues = new List<int>();

        using var reader = new StreamReader(filename);
        while (reader.ReadLine() is { } line)
        {
            var firstspace = line.IndexOf(' ');
            var lastspace = line.LastIndexOf(' ');
            leftValues.Add(int.Parse(line[..firstspace]));
            rightValues.Add(int.Parse(line[(lastspace + 1)..]));
        }

        leftValues.Sort();
        rightValues.Sort();

        var distance = 0;
        for (var i = 0; i < leftValues.Count; i++)
        {
            var dist = Math.Abs(leftValues[i] - rightValues[i]);
            distance += dist;
        }

        return distance;
    }

    public static int Part_2(string filename)
    {
        var leftValues = new List<int>();
        var rightValues = new Dictionary<int, int>();
        using var reader = new StreamReader(filename);
        while (reader.ReadLine() is { } line)
        {
            var firstspace = line.IndexOf(' ');
            var lastspace = line.LastIndexOf(' ');
            var left = int.Parse(line[..firstspace]);
            var right = int.Parse(line[(lastspace + 1)..]);
            leftValues.Add(left);
            if (rightValues.TryGetValue(right, out int value))
            {
                rightValues[right] = ++value;
            }
            else
            {
                rightValues[right] = 1;
            }
        }

        var similarity = 0;

        foreach (var left in leftValues)
        {
            if (rightValues.TryGetValue(left, out int value))
            {
                similarity += value * left;
            }
        }

        return similarity;
    }
}
