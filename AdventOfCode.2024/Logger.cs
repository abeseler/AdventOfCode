using System.Diagnostics;

namespace AdventOfCode2024;

internal static class Logger
{
    public static void MeasureAndLog(Func<string, int> action, string inputFile, int iterations = 1, bool warmuup = true)
    {
        if (warmuup)
        {
            for (int i = 0; i < 10; i++)
            {
                _ = action(inputFile);
            }
        }
        var result = 0;
        var start = Stopwatch.GetTimestamp();
        var startingMemory = GC.GetAllocatedBytesForCurrentThread();
        for (int i = 0; i < iterations; i++)
        {
            result = action(inputFile);
        }
        var duration = Stopwatch.GetElapsedTime(start);
        var endingMemory = GC.GetAllocatedBytesForCurrentThread();
                
        Console.WriteLine($"""
            {action.Method.DeclaringType?.Name + "." + action.Method.Name} -> {inputFile.Replace(action.Method.DeclaringType?.Name ?? "", "").Replace("/", "")}
              [Dur] {Math.Round(duration.TotalMicroseconds / iterations, 2)} µs
              [Mem] {Math.Round((endingMemory - startingMemory) / iterations / 1024.0, 2)} KB
              [Res] {result}

            """);
    }

    public static void MeasureAndLog(Func<string, long> action, string inputFile, int iterations = 1, bool warmuup = true)
    {
        if (warmuup)
        {
            for (int i = 0; i < 10; i++)
            {
                _ = action(inputFile);
            }
        }
        long result = 0;
        var start = Stopwatch.GetTimestamp();
        var startingMemory = GC.GetAllocatedBytesForCurrentThread();
        for (int i = 0; i < iterations; i++)
        {
            result = action(inputFile);
        }
        var duration = Stopwatch.GetElapsedTime(start);
        var endingMemory = GC.GetAllocatedBytesForCurrentThread();

        Console.WriteLine($"""
            {action.Method.DeclaringType?.Name + "." + action.Method.Name} -> {inputFile.Replace(action.Method.DeclaringType?.Name ?? "", "").Replace("/", "")}
              [Dur] {Math.Round(duration.TotalMicroseconds / iterations, 2)} µs
              [Mem] {Math.Round((endingMemory - startingMemory) / iterations / 1024.0, 2)} KB
              [Res] {result}

            """);
    }
}
