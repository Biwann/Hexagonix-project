using DG.Tweening;
using System.Drawing;
using System.Linq;
using UnityEngine;

public abstract class PlacebleObjectBase : MonoBehaviour, IPlacebleObject
{
    public bool IsPlaced { get; private set; } = false;

    public bool CanPlace()
    {
        var cell = GetCellToPlaceComponent();
        return cell != null && cell.IsEmpty;
    }

    public void Place()
    {
        transform.DOComplete();
        if (!CanPlace())
        {
            Debug.Log($"Cant see cell on {_fieldPosition}");
            return;
        }

        var cell = GetCellToPlaceComponent();

        cell.TryPlace(this,
            onSuccess: position =>
        {
            transform.DOMove(
                endValue: new Vector3(position.x, position.y, position.z - 1),
                duration: 0.5f);
            transform.DOShakeScale(
                duration: 0.5f,
                strength: 0.1f,
                vibrato: 5);
            transform.parent = cell.transform;
            IsPlaced = true;
        });
    }

    public Cell GetCellToPlaceComponent()
    {
        Debug.Log($"{gameObject.name}: position ({transform.position.x}, {transform.position.y})");
        var pos = new Vector2(transform.position.x, transform.position.y);
        var cells = Physics2D.RaycastAll(pos, Vector2.up, 0.01f);
        var cell = cells.Select(o => o.collider?.gameObject.GetComponent<Cell>()).FirstOrDefault(a => a != null);

        return cell;
    }

    public void DestroyObject()
    {
        if (IsPlaced)
        {
            IsPlaced = false;
            DestroyObjectImpl();
        }
    }

    public abstract int GetPoints();

    public Point GetLocalFieldPosition() => _fieldPosition;

    public void SetLocalFieldPosition(Point position) => _fieldPosition = position;

    public GameObject GetGameObject()
        => gameObject;

    protected virtual void DestroyObjectImpl()
    {
        DestroyImmediate(gameObject);
    }

    private Point _fieldPosition;
}