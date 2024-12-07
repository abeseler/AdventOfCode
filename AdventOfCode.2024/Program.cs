using AdventOfCode2024;

var puzzleSolutions = typeof(Program).Assembly.GetTypes()
    .Where(t => t.GetInterfaces().Contains(typeof(PuzzleSolution)))
    .OrderBy(t => t.Name)
    .ToList();

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
