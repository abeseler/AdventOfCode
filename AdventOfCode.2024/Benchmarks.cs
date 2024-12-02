using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AdventOfCode2024;

[MemoryDiagnoser(true), ShortRunJob]
public class Benchmark
{
    #region Day 1
    [Benchmark]
    public void Day_1_1()
    {
        Day_01.Part_1("Day_01/Input.txt");
    }

    [Benchmark]
    public void Day_1_2()
    {
        Day_01.Part_2("Day_01/Input.txt");
    }
    #endregion
}
