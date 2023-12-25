using AdventOfCode_2023;
using AdventOfCode_2023.Day_1;
using AdventOfCode_2023.Day_2;
using AdventOfCode_2023.Day_3;
using BenchmarkDotNet.Running;

var runBenchmarks = false;
if (runBenchmarks)
{
    BenchmarkRunner.Run<Benchmark>();
    return;
}
else
{
    Logger.MeasureAndLog(TrebuchetCalibrationDocParser.GetCalibrationSum, "Day_1/Input.txt", 10);
    Logger.MeasureAndLog(TrebuchetCalibrationDocParserYoutube.GetCalibrationSum, "Day_1/Input.txt", 10);
    Logger.MeasureAndLog(TrebuchetCalibrationDocParser.GetCalibrationSumIncludeTokens, "Day_1/Input.txt", 10);
    Logger.MeasureAndLog(TrebuchetCalibrationDocParserYoutube.GetCalibrationSumIncludeTokens, "Day_1/Input.txt", 10);
    Logger.MeasureAndLog(CubeGameResultsProcessor.GetSumOfPossibleGames, "Day_2/Input.txt", 10);
    Logger.MeasureAndLog(CubeGameResultsProcessorYoutube.GetSumOfPossibleGames, "Day_2/Input.txt", 10);
    Logger.MeasureAndLog(CubeGameResultsProcessor.GetSumOfMinimumCubePowers, "Day_2/Input.txt", 10);
    Logger.MeasureAndLog(CubeGameResultsProcessorYoutube.GetSumOfMinimumCubePowers, "Day_2/Input.txt", 10);
    Logger.MeasureAndLog(EngineSchematicReader.GetSumOfPartNumbers, "Day_3/Input.txt", 10);
    Logger.MeasureAndLog(EngineSchematicReader.GetSumOfGearRatios, "Day_3/Sample.txt", 10);
}
