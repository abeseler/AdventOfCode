using AdventOfCode_2023.Day_1;
using AdventOfCode_2023.Day_2;
using AdventOfCode_2023.Day_3;
using AdventOfCode_2023.Day_4;
using AdventOfCode_2023.Day_5;

namespace AdventOfCode_2023.Tests;

public sealed class Tests
{
    [Fact]
    public void Day_1_1()
    {
        var result = TrebuchetCalibrationDocParser.GetCalibrationSum("Day_1/Sample.txt");
        result.Should().Be(142);
    }
    [Fact]
    public void Day_1_2()
    {
        var result = TrebuchetCalibrationDocParser.GetCalibrationSumIncludeTokens("Day_1/Sample2.txt");
        result.Should().Be(281);
    }
    [Fact]
    public void Day_2_1()
    {
        var result = CubeGameResultsProcessor.GetSumOfPossibleGames("Day_2/Sample.txt");
        result.Should().Be(8);
    }
    [Fact]
    public void Day_2_2()
    {
        var result = CubeGameResultsProcessor.GetSumOfMinimumCubePowers("Day_2/Sample.txt");
        result.Should().Be(2286);
    }
    [Fact]
    public void Day_3_1()
    {
        var result = EngineSchematicReader.GetSumOfPartNumbers("Day_3/Sample.txt");
        result.Should().Be(4361);
    }
    [Fact]
    public void Day_3_2()
    {
        var result = EngineSchematicReader.GetSumOfGearRatios("Day_3/Sample.txt");
        result.Should().Be(467835);
    }
    [Fact]
    public void Day_4_1()
    {
        var result = ScratchcardScratcher.GetTotalPoints("Day_4/Sample.txt");
        result.Should().Be(13);
    }
    [Fact]
    public void Day_4_2()
    {
        var result = ScratchcardScratcher.GetCardCount("Day_4/Sample.txt");
        result.Should().Be(30);
    }
    [Fact]
    public void Day_5_1()
    {
        var result = AlmanacDemystifier.GetLowestSeedLocation("Day_5/Sample.txt");
        result.Should().Be(35);
    }
}
