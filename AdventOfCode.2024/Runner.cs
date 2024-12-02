using BenchmarkDotNet.Running;

namespace AdventOfCode2024;

internal static class Runner
{
    public static void Run()
    {
        //BenchmarkRunner.Run<Benchmark>();

        Logger.MeasureAndLog(Day_01.Part_1, "Day_01/Example.txt", 1, true);
        Logger.MeasureAndLog(Day_01.Part_1, "Day_01/Input.txt", 1, false);
        Logger.MeasureAndLog(Day_01.Part_2, "Day_01/Example.txt", 1, true);
        Logger.MeasureAndLog(Day_01.Part_2, "Day_01/Input.txt", 1, false);
        Logger.MeasureAndLog(Day_02.Part_1, "Day_02/Example.txt", 1, true);
        Logger.MeasureAndLog(Day_02.Part_1, "Day_02/Input.txt", 1, false);
        Logger.MeasureAndLog(Day_02.Part_2, "Day_02/Example.txt", 1, true);
        Logger.MeasureAndLog(Day_02.Part_2, "Day_02/Input.txt", 1, false);
    }
}
