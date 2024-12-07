namespace AdventOfCode2024;

internal interface PuzzleSolution
{
    static abstract string Name { get; }
    static abstract string FileName { get; }
    static abstract string TestFileName { get; }
    static abstract string TestOutputExpected { get; }
    static abstract string Solve(StreamReader reader);
}
