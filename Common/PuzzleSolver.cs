namespace AdventOfCode;

public static class PuzzleSolver
{
    private static readonly ConsoleColor[] colors = [
        ConsoleColor.Green,
        ConsoleColor.Red,
    ];
    private static int colorIndex = 0;

    public static void SolveAll(Type type)
    {
        var puzzleSolutions = type.Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(PuzzleSolution))).OrderBy(t => t.Name);
        foreach (var puzzleSolution in puzzleSolutions)
        {
            try
            {
                typeof(PuzzleSolver)
                    .GetMethods()
                    .FirstOrDefault(m => m.Name == nameof(PuzzleSolution.Solve) && m.IsGenericMethod)?
                    .MakeGenericMethod(puzzleSolution)
                    .Invoke(null, null);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while solving {puzzleSolution.Name}: {e.Message}");
            }
        }

        Console.ForegroundColor = ConsoleColor.Gray;
    }

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
            Console.WriteLine($"{typeof(T).Name} - Test failed. Expected ({T.TestOutputExpected}) but got ({result})");
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
