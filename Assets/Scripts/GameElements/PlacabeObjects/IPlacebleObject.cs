using System.Drawing;

public interface IPlacebleObject
{
    Point GetLocalPosition();

    void SetPosition(Point position);

    bool CanPlace();

    void Place();

    void DestroyObject();

    int GetPoints();
}
