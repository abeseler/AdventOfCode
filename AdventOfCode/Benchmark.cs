using AdventOfCode_2023.Day_1;
using AdventOfCode_2023.Day_2;
using AdventOfCode_2023.Day_3;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode_2023;

[MemoryDiagnoser(true)]
public class Benchmark
{
    [Benchmark]
    public void Day_1_Part_1()
    {
        TrebuchetCalibrationDocParser.GetCalibrationSum("Day_1/CalibrationDocument.txt");
    }
    [Benchmark]
    public void Day_1_Part_1_Youtube()
    {
        TrebuchetCalibrationDocParserYoutube.GetCalibrationSum("Day_1/CalibrationDocument.txt");
    }

    [Benchmark]
    public void Day_1_Part_2()
    {
        TrebuchetCalibrationDocParser.GetCalibrationSumIncludeTokens("Day_1/CalibrationDocument.txt");
    }
    [Benchmark]
    public void Day_1_Part_2_Youtube()
    {
        TrebuchetCalibrationDocParserYoutube.GetCalibrationSumIncludeTokens("Day_1/CalibrationDocument.txt");
    }

    [Benchmark]
    public void Day_2_Part_1()
    {
        CubeGameResultsProcessor.GetSumOfPossibleGames("Day_2/CubeConundrum.txt");
    }

    [Benchmark]
    public void Day_2_Part_2()
    {
        CubeGameResultsProcessor.GetSumOfMinimumCubePowers("Day_2/CubeConundrum.txt");
    }

    [Benchmark]
    public void Day_3_Part_1()
    {
        EngineSchematicReader.GetSumOfPartNumbers("Day_3/EngineSchematic.txt");
    }
}
