using System.Numerics;

namespace AdventOfCode.Utils;

public static class Vector2Ext
{
    public static IEnumerable<Vector2> Neighbors(this Vector2 v)
    {
        yield return new Vector2(v.X + 1, v.Y);
        yield return new Vector2(v.X - 1, v.Y);
        yield return new Vector2(v.X, v.Y + 1);
        yield return new Vector2(v.X, v.Y - 1);
    }

    public static IEnumerable<Vector2> NeighborsWithDiagonals(this Vector2 v)
    {
        yield return new Vector2(v.X + 1, v.Y);
        yield return new Vector2(v.X - 1, v.Y);
        yield return new Vector2(v.X, v.Y + 1);
        yield return new Vector2(v.X, v.Y - 1);
        yield return new Vector2(v.X + 1, v.Y + 1);
        yield return new Vector2(v.X - 1, v.Y - 1);
        yield return new Vector2(v.X + 1, v.Y - 1);
        yield return new Vector2(v.X - 1, v.Y + 1);
    }

    public static Vector2 Up(this Vector2 v) => new(v.X, v.Y - 1);
    public static Vector2 Down(this Vector2 v) => new(v.X, v.Y + 1);
    public static Vector2 Left(this Vector2 v) => new(v.X - 1, v.Y);
    public static Vector2 Right(this Vector2 v) => new(v.X + 1, v.Y);
    public static Vector2 UpLeft(this Vector2 v) => new(v.X - 1, v.Y - 1);
    public static Vector2 UpRight(this Vector2 v) => new(v.X + 1, v.Y - 1);
    public static Vector2 DownLeft(this Vector2 v) => new(v.X - 1, v.Y + 1);
    public static Vector2 DownRight(this Vector2 v) => new(v.X + 1, v.Y + 1);

}
