using System.Numerics;

namespace AdventOfCode.Utils;

public static class Vector2Ext
{
    public static IEnumerable<Vector2> Neighbours(this Vector2 v)
    {
        yield return new Vector2(v.X + 1, v.Y);
        yield return new Vector2(v.X - 1, v.Y);
        yield return new Vector2(v.X, v.Y + 1);
        yield return new Vector2(v.X, v.Y - 1);
    }

    public static IEnumerable<Vector2> NeighboursWithDiagonals(this Vector2 v)
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
}
