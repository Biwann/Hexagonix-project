using UnityEngine;

public class HexagonixFieldProvider
{
    public int Radius { get; } = 4;

    public bool IsCoordinateOnField(int x, int y)
    {
        return !(
            y % 2 == 0
            ? Mathf.Abs(x) > Radius - Mathf.Abs(y) / 2
            : x < -(Radius - Mathf.Abs(y) / 2) || x >= Radius - Mathf.Abs(y) / 2);
    }
}