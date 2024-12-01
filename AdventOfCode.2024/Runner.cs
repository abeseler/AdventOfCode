namespace AdventOfCode2024;

internal static class Runner
{
    public static void Run()
    {        
        Logger.MeasureAndLog(Day_01.CalculateDistance, "Day_01/Example.txt");
        Logger.MeasureAndLog(Day_01.CalculateDistance, "Day_01/Input.txt");
        Logger.MeasureAndLog(Day_01.CalculateSimilarity, "Day_01/Example.txt");
        Logger.MeasureAndLog(Day_01.CalculateSimilarity, "Day_01/Input.txt");
    }
}
