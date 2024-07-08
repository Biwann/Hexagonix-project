using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;
using Zenject;

public sealed class Figure : MonoBehaviour
{
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
        _camera = FindObjectOfType<Camera>();
        _startPosition = transform.position;
    }

    public void OnMouseDrag()
    {
        SetWorldCoordinatesFromMousePosition();
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

    private void SetWorldCoordinatesFromMousePosition()
    {
        var mp = Input.mousePosition;
        var position = _camera.ScreenToWorldPoint(mp);

        // _startPosition.z - 5 neccessary to be above all placed objects
        transform.position = new Vector3(position.x, position.y + 1, _startPosition.z - 5);
    }

    private Camera _camera;
    private Vector3 _startPosition;
    private Tracer _tracer;
    [SerializeField, ReadOnly]
    private List<PlacebleObjectBase> _objectsInFigure = new();
}