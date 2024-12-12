namespace AdventOfCode;

public interface PuzzleSolution
{
    static abstract string FileName { get; }
    static abstract string TestFileName { get; }
    static abstract string TestOutputExpected { get; }
    static abstract string Solve(StreamReader reader);
}
