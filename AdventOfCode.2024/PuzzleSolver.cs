namespace AdventOfCode2024;

internal static class PuzzleSolver
{
    private static ConsoleColor[] colors = [
        ConsoleColor.Green,
        ConsoleColor.Red,
    ];
    private static int colorIndex = 0;
    public static void Solve<T>() where T : PuzzleSolution
    {
        Console.ForegroundColor = colors[colorIndex];
        colorIndex = (colorIndex + 1) % colors.Length;
        var result = Solve<T>(T.TestFileName);

        if (string.IsNullOrEmpty(result))
        {
            return;
        }

        if (result != T.TestOutputExpected)
        {
            Console.WriteLine($"{T.Name} - Test failed. Expected: {T.TestOutputExpected}, but got: {result}");
            return;
        }

        result = Solve<T>(T.FileName);

        Console.WriteLine($"{T.Name} - {result}");
    }

    public static string Solve<T>(string fileName) where T : PuzzleSolution
    {
        using var fileReader = new StreamReader(fileName);
        return T.Solve(fileReader);        
    }
}
