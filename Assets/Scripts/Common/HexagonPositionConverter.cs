using System.Drawing;
using UnityEngine;

public static class HexagonPositionConverter
{
    const float OFFSET = 0;
    const float PREFAB_HEIGHT = 1.048f;
    const float PREFAB_WIDTH = 0.9f;

    public static Vector3 GetRealPosition(Vector3 startPosition, int x, int y, float scale = 1f)
    {
        float realX = startPosition.x + x * (PREFAB_WIDTH + OFFSET) + (y % 2 == 0 ? 0 : PREFAB_WIDTH / 2);
        float realY = startPosition.y + y * (PREFAB_HEIGHT * 3 / 4 + OFFSET);

        return new Vector3(realX * scale, realY * scale, startPosition.z);
    }

    public static Vector3 GetRealPosition(Vector3 startPosition, Point coordinates, float scale = 1f)
        => GetRealPosition(startPosition, coordinates.X, coordinates.Y, scale);
}