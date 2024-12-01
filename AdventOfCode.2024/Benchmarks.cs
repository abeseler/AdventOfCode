using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AdventOfCode2024;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser(true)]
[Config(typeof(Config))]
public class Benchmark
{
    #region Day 1
    [BenchmarkCategory("Day_1_1"), Benchmark(Baseline = true)]
    public void Day_1_1()
    {
        Day_01.CalculateDistance("Day_01/Input.txt");
    }
    [BenchmarkCategory("Day_1_1"), Benchmark]
    public void Day_1_2()
    {
        Day_01.CalculateSimilarity("Day_01/Input.txt");
    }
    
    #endregion

    public class Config : ManualConfig
    {
        public Config()
        {
            SummaryStyle = BenchmarkDotNet.Reports.SummaryStyle.Default.WithRatioStyle(BenchmarkDotNet.Columns.RatioStyle.Trend);
        }
    }
}
