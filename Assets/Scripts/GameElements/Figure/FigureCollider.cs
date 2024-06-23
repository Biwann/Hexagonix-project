using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using Zenject;

public sealed class FigureCollider : MonoBehaviour
{
    [SerializeField] Camera _camera;
    public event Action FigurePlaced;

    [Inject]
    public void Inject(Tracer tracer)
    {
        _tracer = tracer;
    }

    public void Init(List<PlacebleObjectBase> objectsInFigure)
    {
        _objectsInFigure = objectsInFigure;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void OnMouseDrag()
    {
        // TODO: fix position
        //var mousePosition2D = inpu
        var position = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(position.x, position.y + 1, _startPosition.z - 10);
    }

    public void OnMouseUp()
    {
        Debug.Log("Mouse UP");
        PlaceObjects();
        transform.position = _startPosition;
    }

    private bool CanPlaceObjects()
        => _objectsInFigure.All(o => o.CanPlace());

    private void PlaceObjects()
    {
        if (!CanPlaceObjects())
        {
            Debug.Log("CANT PLACE");
            return;
        }
        Debug.Log($"PLACING {_objectsInFigure.Count} objects" );
        _objectsInFigure.ForEach(o => o.Place());
        _objectsInFigure.Clear();
        FigurePlaced?.Invoke();
    }

    private Vector3 _startPosition;
    private Tracer _tracer;
    [SerializeField, ReadOnly]
    private List<PlacebleObjectBase> _objectsInFigure = new();
}