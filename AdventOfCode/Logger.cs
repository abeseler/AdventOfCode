using System.Diagnostics;
using System.Text.Json;

namespace AdventOfCode_2023;

internal static class Logger
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true
    };

    public static void MeasureAndLog(Func<string, int> action, string filename, int iterations = 1, bool warmuup = true)
    {
        if (warmuup)
        {
            for (int i = 0; i < 10; i++)
            {
                _ = action(filename);
            }
        }        
        var result = 0;
        var start = Stopwatch.GetTimestamp();
        for (int i = 0; i < iterations; i++)
        {
            result = action(filename);
        }
        var duration = Stopwatch.GetElapsedTime(start);

        Console.WriteLine(JsonSerializer.Serialize(new
        {
            Action = action.Method.DeclaringType?.Name + "." + action.Method.Name,
            Duration = Math.Round(duration.TotalMicroseconds / iterations, 2),
            Result = result
        }, _jsonOptions));
    }
}
