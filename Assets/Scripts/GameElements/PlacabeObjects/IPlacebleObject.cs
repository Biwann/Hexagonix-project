using System.Drawing;
using UnityEngine;

public interface IPlacebleObject
{
    Point GetLocalFieldPosition();

    void SetLocalFieldPosition(Point position);

    bool CanPlace();

    void Place();

    void DestroyObject();

    int GetPoints();

    GameObject GetGameObject();
}
