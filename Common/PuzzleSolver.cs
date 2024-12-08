namespace AdventOfCode;

public static class PuzzleSolver
{
    public static void SolveAll(Type type)
    {
        ReadOnlySpan<ConsoleColor> colors = [ConsoleColor.Green, ConsoleColor.Red];
        var colorIndex = 0;

        var puzzleSolutions = type.Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(PuzzleSolution))).OrderBy(t => t.Name);        
        foreach (var puzzleSolution in puzzleSolutions)
        {
            var result = typeof(PuzzleSolver)
                .GetMethods()
                .FirstOrDefault(m => m.Name == nameof(Solve) && m.IsGenericMethod)?
                .MakeGenericMethod(puzzleSolution)
                .Invoke(null, null);

            Console.ForegroundColor = colors[colorIndex];
            Console.WriteLine(result);

            colorIndex = (colorIndex + 1) % colors.Length;
        }
        Console.ResetColor();
    }

    public static string Solve<T>() where T : PuzzleSolution
    {
        try
        {
            var result = Solve<T>(T.TestFileName);
            if (string.IsNullOrEmpty(result)) return result;
            if (result != T.TestOutputExpected)
            {
                return $"{typeof(T).Name} - Test failed. Expected ({T.TestOutputExpected}) but got ({result})";
            }
        }
        catch (FileNotFoundException)
        {
            return $"{typeof(T).Name} - No test file found. Skipping test.";
        }
        catch (Exception e)
        {
            return $"{typeof(T).Name} - Test failed. {e.Message}";
        }

        try
        {
            var result = Solve<T>(T.FileName);
            return $"{typeof(T).Name} - {result}";
        }
        catch (FileNotFoundException)
        {
            return $"{typeof(T).Name} - No file found.";
        }
        catch (Exception e)
        {
            return $"{typeof(T).Name} - {e.Message}";
        }
    }

    public static string Solve<T>(string fileName) where T : PuzzleSolution
    {
        using var fileReader = new StreamReader(fileName);
        return T.Solve(fileReader);
    }
}
