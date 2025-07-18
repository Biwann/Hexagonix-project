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
        _defaultColor = GetColor();
    }

    public void ChangeColor(UnityEngine.Color color)
    {
        var renderer = GetSpriteRenderer();
        renderer.color = color;
    }

    public void SetDefaultColor()
        => ChangeColor(_defaultColor);

    public UnityEngine.Color GetColor()
        => GetSpriteRenderer().color;

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

    public Point Position => _cellInformation.Position;

    public bool IsEmpty => _cellInformation.IsEmpty;

    public GameObject GetPlacedGameObject() => _cellInformation.GetPlacedGameObject();

    private SpriteRenderer GetSpriteRenderer()
        => _cachedSpriteRenderer ??= gameObject.GetComponentInChildren<SpriteRenderer>();

    private CellInformation _cellInformation;
    private UnityEngine.Color _defaultColor;
    private Tracer _tracer;
    private SpriteRenderer _cachedSpriteRenderer;
}
