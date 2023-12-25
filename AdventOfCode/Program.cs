using AdventOfCode_2023;
using AdventOfCode_2023.Day_1;
using AdventOfCode_2023.Day_2;
using AdventOfCode_2023.Day_3;
using BenchmarkDotNet.Running;
using System.Diagnostics;

var runBenchmarks = false;
if (runBenchmarks)
{
    BenchmarkRunner.Run<Benchmark>();
    return;
}
else
{
    MeasureAndLog(TrebuchetCalibrationDocParser.GetCalibrationSum, "Day_1/Input.txt", 10);
    MeasureAndLog(TrebuchetCalibrationDocParserYoutube.GetCalibrationSum, "Day_1/Input.txt", 10);
    MeasureAndLog(TrebuchetCalibrationDocParser.GetCalibrationSumIncludeTokens, "Day_1/Input.txt", 10);
    MeasureAndLog(TrebuchetCalibrationDocParserYoutube.GetCalibrationSumIncludeTokens, "Day_1/Input.txt", 10);
    MeasureAndLog(CubeGameResultsProcessor.GetSumOfPossibleGames, "Day_2/Input.txt", 10);
    MeasureAndLog(CubeGameResultsProcessorYoutube.GetSumOfPossibleGames, "Day_2/Input.txt", 10);
    MeasureAndLog(CubeGameResultsProcessor.GetSumOfMinimumCubePowers, "Day_2/Input.txt", 10);
    MeasureAndLog(CubeGameResultsProcessorYoutube.GetSumOfMinimumCubePowers, "Day_2/Input.txt", 10);
    MeasureAndLog(EngineSchematicReader.GetSumOfPartNumbers, "Day_3/Input.txt", 10);
    MeasureAndLog(EngineSchematicReader.GetSumOfGearRatios, "Day_3/Sample.txt", 10);
}

static void MeasureAndLog(Func<string, int> action, string filename, int iterations = 1)
{
    for (int i = 0; i < 10; i++)
    {
        _ = action(filename); // Warmup
    }
    var result = 0;
    var start = Stopwatch.GetTimestamp();
    for (int i = 0; i < iterations; i++)
    {
        result = action(filename);
    }
    var end = Stopwatch.GetTimestamp();

    Console.WriteLine($"""
        [{action.Method.DeclaringType?.Name}.{action.Method.Name}] ({Math.Round(Stopwatch.GetElapsedTime(start, end).TotalMicroseconds / iterations)} μs) Result: {result}
        """);
}
