using UnityEngine;

public class ColorProvider
{
    public Color GetRandomColor()
        => _colors.SelectRandom();

    private readonly Color[] _colors =
    {
        Color.green,
        Color.red,
        Color.blue,
        Color.yellow,
        // purple
        new Color(128, 0, 255),
    };
}