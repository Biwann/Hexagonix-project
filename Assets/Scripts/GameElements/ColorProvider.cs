using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorProvider
{
    public Color GetNextColor()
    {
        var choosedColor = _colors.Except(_lastUsedColors).SelectRandom();
        _lastUsedColors.Enqueue(choosedColor);

        var colorsAmount = _colors.Length;
        var maxUsedFigures = MAX_LAST_USED_COLORS < colorsAmount
            ? MAX_LAST_USED_COLORS
            : colorsAmount - 1;

        if (_lastUsedColors.Count > maxUsedFigures)
        {
            _lastUsedColors.Dequeue();
        }

        return choosedColor;
    }

    private readonly Color[] _colors =
    {
        NewColor(0f, 255f, 102f),    // green
        NewColor(0f, 255f, 255f),    // blue
        NewColor(255f, 255f, 102f),  // yellow
        NewColor(255f, 102f, 251f),  // purple
    };

    private static Color NewColor(float r, float g, float b)
        => new Color(r * COLOR_COEF, g * COLOR_COEF, b * COLOR_COEF);

    private const float COLOR_COEF = 1f / 255f;
    private const int MAX_LAST_USED_COLORS = 1;
    private Queue<Color> _lastUsedColors = new();
}