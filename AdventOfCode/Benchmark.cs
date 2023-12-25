using AdventOfCode_2023.Day_1;
using AdventOfCode_2023.Day_2;
using AdventOfCode_2023.Day_3;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AdventOfCode_2023;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser(true)]
[Config(typeof(Config))]
public class Benchmark
{
    [BenchmarkCategory("Day_1_1"), Benchmark(Baseline = true)]
    public void Day_1_Part_1()
    {
        TrebuchetCalibrationDocParser.GetCalibrationSum("Day_1/CalibrationDocument.txt");
    }
    [BenchmarkCategory("Day_1_1"), Benchmark]
    public void Day_1_Part_1_Youtube()
    {
        TrebuchetCalibrationDocParserYoutube.GetCalibrationSum("Day_1/CalibrationDocument.txt");
    }
    [BenchmarkCategory("Day_1_2"), Benchmark(Baseline = true)]
    public void Day_1_Part_2()
    {
        TrebuchetCalibrationDocParser.GetCalibrationSumIncludeTokens("Day_1/CalibrationDocument.txt");
    }
    [BenchmarkCategory("Day_1_2"), Benchmark]
    public void Day_1_Part_2_Youtube()
    {
        TrebuchetCalibrationDocParserYoutube.GetCalibrationSumIncludeTokens("Day_1/CalibrationDocument.txt");
    }

    [BenchmarkCategory("Day_2_1"), Benchmark(Baseline = true)]
    public void Day_2_Part_1()
    {
        CubeGameResultsProcessor.GetSumOfPossibleGames("Day_2/CubeConundrum.txt");
    }
    [BenchmarkCategory("Day_2_1"), Benchmark]
    public void Day_2_Part_1_Youtube()
    {
        CubeGameResultsProcessorYoutube.GetSumOfPossibleGames("Day_2/CubeConundrum.txt");
    }
    [BenchmarkCategory("Day_2_2"), Benchmark(Baseline = true)]
    public void Day_2_Part_2()
    {
        CubeGameResultsProcessor.GetSumOfMinimumCubePowers("Day_2/CubeConundrum.txt");
    }
    [BenchmarkCategory("Day_2_2"), Benchmark]
    public void Day_2_Part_2_Youtube()
    {
        CubeGameResultsProcessorYoutube.GetSumOfMinimumCubePowers("Day_2/CubeConundrum.txt");
    }

    [BenchmarkCategory("Day_3_1"), Benchmark]
    public void Day_3_Part_1()
    {
        EngineSchematicReader.GetSumOfPartNumbers("Day_3/EngineSchematic.txt");
    }
    //[BenchmarkCategory("Day_3"), Benchmark]
    //public void Day_3_Part_2()
    //{
    //    EngineSchematicReader.GetSumOfGearRatios("Day_3/EngineSchematic.txt");
    //}

    public class Config : ManualConfig
    {
        public Config()
        {
            SummaryStyle = BenchmarkDotNet.Reports.SummaryStyle.Default.WithRatioStyle(BenchmarkDotNet.Columns.RatioStyle.Trend);
        }
    }
}
