using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class Figure : MonoBehaviour
{
    public event Action FigurePlaced;

    public bool CanInteract { get; set; }

    public List<PlacebleObjectBase> ObjectsInFigure { get; private set; } = new();

    public void Inject(ScoresOnLevel score)
    {
        _score = score;
    }

    public void Init(
        List<PlacebleObjectBase> objectsInFigure,
        Color currentColor)
    {
        ObjectsInFigure = objectsInFigure;
        _currentColor = currentColor;

        AlignFiguresInCenter(NORMAL_WAITING_SCALE);
        ObjectsInFigure.ForEach(o => o.transform.DOComplete());
    }

    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
        _startPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (!CanInteract) return;

        AlignFiguresInCenter(NORMAL_SCALE);
        StartTrackingCanPlaceCell();
    }

    private void OnMouseDrag()
    {
        if (!CanInteract) return;

        SetWorldCoordinatesFromMousePosition();
    }

    private void OnMouseUp()
    {
        StopTrackingCanPlaceCell();

        if (!CanInteract) return;

        Debug.Log("Mouse UP");
        var placed = PlaceObjects();

        if (placed)
        {
            transform.position = _startPosition;
        }
        else
        {
            AlignFiguresInCenter(NORMAL_WAITING_SCALE);
            transform.DOMove(_startPosition, duration: 0.1f);
        }
    }

    private void StartTrackingCanPlaceCell()
    {
        _cellTracker = StartCoroutine(TrackCanPlaceCell());
    }

    private void StopTrackingCanPlaceCell()
    {
        if (_cellTracker is not null)
        {
            StopCoroutine(_cellTracker);
            _cellTracker = null;
            ReturnColorAndClearCachedCells();
        }
    }

    private bool CanPlaceObjects()
        => ObjectsInFigure.All(o => o.CanPlace());

    private bool PlaceObjects()
    {
        if (!CanPlaceObjects())
        {
            Debug.Log("CANT PLACE");
            return false;
        }

        Debug.Log($"PLACING {ObjectsInFigure.Count} objects" );
        ObjectsInFigure.ForEach(o => o.Place());

        AddPointsForPlacing();

        ObjectsInFigure.Clear();
        FigurePlaced?.Invoke();

        return true;
    }

    private void SetWorldCoordinatesFromMousePosition()
    {
        var mp = Input.mousePosition;
        var position = _camera.ScreenToWorldPoint(mp);

        transform.position = new Vector3(position.x, position.y + 2, _startPosition.z - 5);
    }

    private void AddPointsForPlacing()
    {
        var points = ObjectsInFigure.Select(o => o.GetPoints()).Sum();
        _score.AddScore(points);
    }

    private void AlignFiguresInCenter(float scale = 1f)
    {
        var objToLocalPositionDictionary = new Dictionary<PlacebleObjectBase, Vector3>();

        var objCount = ObjectsInFigure.Count;
        var sumX = 0f;
        var sumY = 0f;

        // count positions
        foreach (var obj in ObjectsInFigure)
        {
            var localObjectPosition = HexagonPositionConverter.GetRealPosition(
                Vector3.zero, obj.GetLocalFieldPosition(), scale);
            objToLocalPositionDictionary.Add(obj, localObjectPosition);
            
            sumX += localObjectPosition.x;
            sumY += localObjectPosition.y;
        }

        var deltaX = sumX / objCount;
        var deltaY = sumY / objCount;

        // set real positions
        foreach (var obj in ObjectsInFigure)
        {
            var objPosition = objToLocalPositionDictionary[obj];
            var objRealPositionX = objPosition.x - deltaX;
            var objRealPositionY = objPosition.y - deltaY;

            obj.transform.DOComplete();
            obj.transform.DOScale(new Vector3(scale - DELTA_SCALE, scale - DELTA_SCALE), 0.2f);
            obj.transform.DOLocalMove(
                new Vector3(objRealPositionX, objRealPositionY, objPosition.z),
                0.2f);
        }
    }

    private IEnumerator TrackCanPlaceCell()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.05f);
        
            if (!CanPlaceObjects())
            {
                ReturnColorAndClearCachedCells();
                continue;
            }

            List<Cell> currentCells = new();
            ObjectsInFigure.ForEach(p => currentCells.Add(p.GetCellToPlaceComponent()));

            if (!currentCells.SequenceEqual(_savedTrackedCellsWithColor.Select(c => c.Cell)))
            {
                ReturnColorAndClearCachedCells();
                currentCells.ForEach(c =>
                {
                    _savedTrackedCellsWithColor.Add((c, c.GetColor()));
                    c.ChangeColor(_currentColor);
                });
            }
        }
    }

    private void ReturnColorAndClearCachedCells()
    {
        foreach (var (cell, color) in _savedTrackedCellsWithColor)
        {
            cell.ChangeColor(color);
        }

        _savedTrackedCellsWithColor.Clear();
    }

    private const float NORMAL_SCALE = 1f;
    private const float NORMAL_WAITING_SCALE = 0.75f;
    private const float DELTA_SCALE = 0.1f;

    private Camera _camera;
    private Vector3 _startPosition;
    private ScoresOnLevel _score;
    private Color _currentColor;
    private List<(Cell Cell, Color Color)> _savedTrackedCellsWithColor = new();
    private Coroutine _cellTracker;
}