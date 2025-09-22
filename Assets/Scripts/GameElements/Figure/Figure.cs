using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (IsPaused() || !CanInteract) return;

        AlignFiguresInCenter(NORMAL_SCALE);
        StartTrackingCanPlaceCell();
    }

    private void OnMouseDrag()
    {
        if (IsPaused() || !CanInteract) return;

        SetWorldCoordinatesFromMousePosition();
    }

    private void OnMouseUp()
    {
        StopTrackingCanPlaceCell();

        if (IsPaused() || !CanInteract) return;

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
            GameAudioController.Instance.PlayCantPlaceHexagon();
            return false;
        }

        Debug.Log($"PLACING {ObjectsInFigure.Count} objects" );
        ObjectsInFigure.ForEach(o => o.Place());
        GameAudioController.Instance.PlayPlaceHexagon();
        AddPointsForPlacing();

        ObjectsInFigure.Clear();
        FigurePlaced?.Invoke();

        return true;
    }

    private void SetWorldCoordinatesFromMousePosition()
    {
        var mp = Input.mousePosition;
        var position = _camera.ScreenToWorldPoint(mp);

        var movePositionMuliplierX = (position.x - _startPosition.x) / 3;
        var movePositionMuliplierY = (position.y - _startPosition.y) / 2;

        transform.position = new Vector3(
            position.x + movePositionMuliplierX, 
            position.y + movePositionMuliplierY + 4, 
            _startPosition.z - 5);
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
            obj.transform.DOScale(new Vector3(scale - DELTA_SCALE, scale - DELTA_SCALE, 1), 0.2f);
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

            if (!currentCells.SequenceEqual(_savedTrackedCells))
            {
                ReturnColorAndClearCachedCells();
                currentCells.ForEach(c =>
                {
                    _savedTrackedCells.Add(c);
                    // make color darker
                    var changeColor = new Color(
                        _currentColor.r * 0.5f,
                        _currentColor.g * 0.5f,
                        _currentColor.b * 0.5f);

                    c.ChangeColor(changeColor);
                });
            }
        }
    }

    private void ReturnColorAndClearCachedCells()
    {
        foreach (var cell in _savedTrackedCells)
        {
            cell.SetDefaultColor();
        }

        _savedTrackedCells.Clear();
    }

    private bool IsPaused()
        => PauseController.IsPaused;

    private const float NORMAL_SCALE = 1f;
    private const float NORMAL_WAITING_SCALE = 0.75f;
    private const float DELTA_SCALE = 0.1f;

    private Camera _camera;
    private Vector3 _startPosition;
    private ScoresOnLevel _score;
    private Color _currentColor;
    private List<Cell> _savedTrackedCells = new();
    private Coroutine _cellTracker;
}