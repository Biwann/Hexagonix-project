using System;
using System.Drawing;
using UnityEngine;
using Zenject;

public sealed class Cell : MonoBehaviour
{
    [Inject]
    public void Inject(Tracer tracer)
    {
        _tracer = tracer;
    }

    public void Init(CellInformation cellInformation)
    {
        _cellInformation = cellInformation;
    }

    public void ChangeColor(UnityEngine.Color color)
        => GetSpriteRenderer().material.color = color;

    public UnityEngine.Color GetColor()
        => GetSpriteRenderer().material.color;

    public bool TryPlace(IPlacebleObject item, Action<Vector3> onSuccess = null)
    {
        if (!_cellInformation.TryPlaceItem(item))
        {
            _tracer?.TraceWarning($"{item} cant be placed in {_cellInformation.Position}");
            return false;
        }
        
        onSuccess?.Invoke(transform.position);
        _tracer?.TraceDebug($"{item} placed in {_cellInformation.Position}");

        return true;
    }

    public int ClearCellAndGetPoints()
    {
        if (!IsEmpty)
        {
            return _cellInformation.DestroyObjectAndGetPoints();
        }
        return 0;
    }

    private SpriteRenderer GetSpriteRenderer()
        => _cachedSpriteRenderer ??= gameObject.GetComponentInChildren<SpriteRenderer>();

    public Point Position => _cellInformation.Position;

    public bool IsEmpty => _cellInformation.IsEmpty;

    public GameObject GetPlacedGameObject() => _cellInformation.GetPlacedGameObject();

    private CellInformation _cellInformation;
    private Tracer _tracer;
    private SpriteRenderer _cachedSpriteRenderer;
}
