using System.Collections;
using System.Drawing;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class PlacebleObjectBase : MonoBehaviour, IPlacebleObject
{
    [Inject]
    public void Init(Tracer tracer)
    {
        _tracer = tracer;
    }

    public bool IsPlaced { get; private set; } = false;

    public bool CanPlace()
    {
        var cell = GetCellToPlaceComponent();
        return cell != null && cell.IsEmpty;
    }

    public void Place()
    {
        if (!CanPlace())
        {
            _tracer?.TraceWarning($"Cant see cell on {_localPosition}");
        }

        var cell = GetCellToPlaceComponent();

        cell.TryPlace(this,
            onSuccess: position =>
        {
            transform.position = new Vector3(position.x, position.y, position.z - 1);
            transform.parent = cell.transform;
            IsPlaced = true;
        });
    }

    public virtual void DestroyObject()
    {
        if (IsPlaced)
        {
            // add score or make something like that
            // also can destroy neubour cells for example
            DestroyImmediate(gameObject);
            //StartCoroutine(Disappear());
            IsPlaced = false;
        }
    }

    public virtual int GetPoints()
    {
        return 40;
    }

    private IEnumerator Disappear()
    {
        var diff = new Vector3(0.01f, 0.01f, 0f);
        transform.localScale = transform.localScale - diff;
        if (transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(0.01f);
    }

    public Point GetLocalPosition() => _localPosition;

    public void SetPosition(Point position) => _localPosition = position;

    private Cell GetCellToPlaceComponent()
    {
        Debug.Log($"{gameObject.name}: position ({transform.position.x}, {transform.position.y})");
        var pos = new Vector2(transform.position.x, transform.position.y);
        var cells = Physics2D.RaycastAll(pos, Vector2.up, 0.01f);
        var cell =  cells.Select(o => o.collider?.gameObject.GetComponent<Cell>()).FirstOrDefault(a => a != null);

        return cell;
    }

    private Point _localPosition;
    private Tracer _tracer;
}