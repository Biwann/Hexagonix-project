using System.Drawing;

public interface IPlacebleObject
{
    Point GetLocalFieldPosition();

    void SetLocalFieldPosition(Point position);

    bool CanPlace();

    void Place();

    void DestroyObject();

    int GetPoints();
}
