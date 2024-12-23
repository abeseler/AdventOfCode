﻿using System.Collections.Concurrent;
using System.Diagnostics;

namespace AdventOfCode;

public static class PuzzleSolver
{
    private static readonly ConsoleColor[] s_colors = [ConsoleColor.Green, ConsoleColor.Red];
    public static void SolveAll(Type type)
    {
        var puzzleSolutions = type.Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(PuzzleSolution)));
        var results = new ConcurrentDictionary<string, string>();
        
        var start = Stopwatch.GetTimestamp();
        Parallel.ForEach(puzzleSolutions, puzzleSolution =>
        {
            var result = typeof(PuzzleSolver)
                .GetMethods()
                .FirstOrDefault(m => m.Name == nameof(Solve) && m.IsGenericMethod)?
                .MakeGenericMethod(puzzleSolution)
                .Invoke(null, null);

            results.TryAdd(puzzleSolution.Name, (string)result!);
        });

        var colorIndex = 0;
        foreach (var result in results.Where(r => !string.IsNullOrEmpty(r.Value)).OrderBy(r => r.Key))
        {
            Console.ForegroundColor = s_colors[colorIndex];
            Console.WriteLine(result.Value);
            colorIndex = (colorIndex + 1) % s_colors.Length;
        }
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"\nTotal time: {Stopwatch.GetElapsedTime(start)}");

        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public static string Solve<T>() where T : PuzzleSolution
    {
        if (string.IsNullOrEmpty(T.TestOutputExpected)) return "";

        try
        {
            var result = Solve<T>(T.TestFileName);
            if (string.IsNullOrEmpty(result))
            {
                return $"{typeof(T).Name} - Not implemented";
            }
            if (result != T.TestOutputExpected)
            {
                return $"{typeof(T).Name} - Test failed. Expected ({T.TestOutputExpected}) but got ({result})";
            }
        }
        catch (NotImplementedException)
        {
            return $"{typeof(T).Name} - Not implemented";
        }
        catch (Exception e)
        {
            return $"{typeof(T).Name} - Test failed. {e.Message}";
        }

        try
        {
            var start = Stopwatch.GetTimestamp();
            var result = Solve<T>(T.FileName);
            return $"{typeof(T).Name} - {result} [{double.Round(Stopwatch.GetElapsedTime(start).TotalMilliseconds, 2)}ms]";
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
