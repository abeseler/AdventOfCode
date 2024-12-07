namespace AdventOfCode2024;

internal static class PuzzleSolver
{
    public static void Solve<T>() where T : PuzzleSolution
    {
        var result = Solve<T>(T.TestFileName);

        if (string.IsNullOrEmpty(result))
        {
            return;
        }

        if (result != T.TestOutputExpected)
        {
            Console.WriteLine($"Test failed. Expected: {T.TestOutputExpected}, but got: {result}");
            return;
        }

        result = Solve<T>(T.FileName);

        Console.WriteLine($"{typeof(T).Name} - {result}");
    }

    public static string Solve<T>(string fileName) where T : PuzzleSolution
    {
        using var fileReader = new StreamReader(fileName);
        return T.Solve(fileReader);        
    }
}
