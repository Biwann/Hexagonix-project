using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class FigureCreator : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private GameObject _collider;

    public FigureInformation FigureInformation { get; private set; }

    public bool HasFigure { get; private set; }

    [Inject]
    public void Injector(
        FigureProvider figures, 
        ColorProvider colorProvider,
        GameEvents gameEvents,
        FiguresManager figuresManager)
    {
        _figureProvider = figures;
        _colorProvider = colorProvider;
        _gameEvents = gameEvents;
        figuresManager.AddFigure(this);
        figuresManager.ActivateFigures += OnFigureActivate;

        SpawnFigure();
    }

    public void Init(FigureInformation figureInformation)
    {
        Debug.Log("Init");
        FigureInformation = figureInformation;
        CreateFigure();
    }

    private void CreateFigure()
    {
        _objectsInFigure.Clear();
        var startPosition = transform.position;
        var color = _colorProvider.GetRandomColor();
        List<GameObject> childObjects = new();

        foreach(var figure in FigureInformation.Parts)
        {
            var position = HexagonPositionConverter.GetRealPosition(startPosition, figure.Position);
            var cell = Instantiate(_cellPrefab, position, Quaternion.identity);

            childObjects.Add(cell);
            cell.transform.localScale = new Vector3(0.9f, 0.9f);
            cell.name = $"{position.x}, {position.y}";
            cell.GetComponentInChildren<SpriteRenderer>().material.color = color;
            
            var obj = cell.GetComponent<PlacebleObjectBase>();
            _objectsInFigure.Add(obj);
        }

        if (_figureHolder == null)
        {
            _figureHolder = Instantiate(_collider);
            _figureHolder.GetComponent<Figure>().FigurePlaced += OnFiguresPlaced;
            _figureHolder.transform.parent = transform;
            _figureHolder.name = "Figure Collider";
        }

        _figureHolder.GetComponent<Figure>().Init(_objectsInFigure);
        AlignCenterFigures(_figureHolder, childObjects);
        _figureHolder.transform.position = startPosition;
        HasFigure = true;
    }

    private void AlignCenterFigures(GameObject parentObject, List<GameObject> childObjects)
    {
        var count = childObjects.Count;
        var x = childObjects.Sum(f => f.transform.position.x) / count;
        var y = childObjects.Sum(f => f.transform.position.y) / count;

        parentObject.transform.position = new Vector3(x, y);

        childObjects.ForEach(o => o.transform.parent = parentObject.transform);
    }

    private void OnFiguresPlaced()
    {
        HasFigure = false;
        _gameEvents.InvokePlacedOnField();
    }

    private void OnFigureActivate()
    {
        SpawnFigure();
        HasFigure = true;
    }

    private void SpawnFigure()
        => Init(_figureProvider.GetNextFigure());

    private List<PlacebleObjectBase> _objectsInFigure = new();
    private FigureProvider _figureProvider;
    private ColorProvider _colorProvider;
    private GameObject _figureHolder;
    private GameEvents _gameEvents;
    private bool _hasFigure;
}