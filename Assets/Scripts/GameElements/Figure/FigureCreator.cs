using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FigureCreator : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private GameObject _collider;
    [SerializeField] private GameObject _figureBlockerView;

    public FigureInformation FigureInformation { get; private set; }

    public bool HasFigure { get; private set; }

    [Inject]
    public void Injector(
        FigureProvider figures, 
        ColorProvider colorProvider,
        GameEvents gameEvents,
        FiguresManager figuresManager,
        Tracer tracer,
        ScoresOnLevel score)
    {
        _figureHolder = null;
        _figureProvider = figures;
        _colorProvider = colorProvider;
        _gameEvents = gameEvents;
        _tracer = tracer;
        _score = score;

        figuresManager.AddFigure(this);
        figuresManager.ActivateFigures += OnFigureActivate;

        SpawnFigure();
        SetCanInteractWithFigure(true);
    }

    public void Init(FigureInformation figureInformation)
    {
        Debug.Log("Init");
        FigureInformation = figureInformation;
        CreateFigure();
    }

    public void SetAllowPlaceFigure(bool value)
    {
        _tracer.TraceDebug("Setting alow place figure " + value);
        SetCanInteractWithFigure(value);

        _canPlaceFigure = value;
    }

    public bool CanPlaceFigure()
        => _canPlaceFigure;

    private void CreateFigure()
    {
        _objectsInFigure.Clear();
        var color = _colorProvider.GetNextColor();
        List<GameObject> childObjects = new();

        foreach(var figure in FigureInformation.Parts)
        {
            var cell = InstansiateDefaultFigurePart(color);
            cell.name = $"{figure.Position.X}, {figure.Position.Y}";
            childObjects.Add(cell);

            var obj = cell.GetComponent<PlacebleObjectBase>();
            obj.SetLocalFieldPosition(figure.Position);

            _objectsInFigure.Add(obj);
        }

        _figureHolder ??= CreateFigureHolder();
        childObjects.ForEach(o => o.transform.parent = _figureHolder.transform);
        _figureHolder.GetComponent<Figure>().Init(_objectsInFigure);

        HasFigure = true;
    }

    private void OnFiguresPlaced()
    {
        HasFigure = false;
        _gameEvents.InvokeFigurePlaced();
    }

    private void OnFigureActivate()
    {
        SpawnFigure();
        HasFigure = true;
    }

    private void SpawnFigure()
        => Init(_figureProvider.GetNextFigure());

    private void SetCanInteractWithFigure(bool value)
    {
        _tracer.TraceDebug("Setting can interact");
        var figure = _figureHolder?.GetComponent<Figure>();
        if (figure == null)
        {
            _tracer.TraceWarning("SetCanInteractWithFigure figureHolder null reference");
            return;
        }

        figure.CanInteract = value;
        figure.ObjectsInFigure.ForEach(o =>
        {
            var prevColor = o.gameObject.GetComponentInChildren<SpriteRenderer>().material.color;
            var newColor = new UnityEngine.Color(prevColor.r, prevColor.g, prevColor.b,
                value ? 1f : 0.1f);
            o.gameObject.GetComponentInChildren<SpriteRenderer>().material.color = newColor;
        });
    }

    private GameObject CreateFigureHolder()
    {
        var holder = Instantiate(_collider, parent: transform);
        holder.transform.position = transform.position;
        holder.name = "Figure Collider";

        var figure = holder.GetComponent<Figure>();
        figure.FigurePlaced += OnFiguresPlaced;
        figure.Inject(_score);

        return holder;
    }

    private GameObject InstansiateDefaultFigurePart(UnityEngine.Color color)
    {
        var cell = Instantiate(_cellPrefab, transform.position, Quaternion.identity);
        cell.GetComponentInChildren<SpriteRenderer>().material.color = color;
        return cell;
    }

    private List<PlacebleObjectBase> _objectsInFigure = new();
    private FigureProvider _figureProvider;
    private ColorProvider _colorProvider;
    private GameObject _figureHolder;
    private GameEvents _gameEvents;
    private Tracer _tracer;
    private ScoresOnLevel _score;
    private bool _canPlaceFigure;
}