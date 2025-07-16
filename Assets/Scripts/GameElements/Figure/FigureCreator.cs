using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FigureCreator : MonoBehaviour
{ 
    [SerializeField] private GameObject _figureBlockerView;

    public FigureInformation FigureInformation { get; private set; }

    public bool HasFigure { get; private set; }

    [Inject]
    public void Injector(
        FigureProvider figures, 
        ColorProvider colorProvider,
        GameEvents gameEvents,
        FiguresManager figuresManager,
        PlacebleObjectsProvider placebleObjectsProvider,
        Tracer tracer,
        ScoresOnLevel score,
        PrefabLoader prefabLoader)
    {
        _figureHolder = null;
        _figureProvider = figures;
        _colorProvider = colorProvider;
        _gameEvents = gameEvents;
        _placebleObjectsProvider = placebleObjectsProvider;
        _tracer = tracer;
        _score = score;

        _collider = prefabLoader.Figure;

        figuresManager.AddFigure(this);
        figuresManager.ActivateFigures += OnFigureActivate;

        SpawnFigure();
        SetCanInteractWithFigure(true);
    }

    public void Init(FigureInformation figureInformation)
    {
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
            var part = InstansiateFigurePart(color);
            part.name = $"{figure.Position.X}, {figure.Position.Y}";
            childObjects.Add(part);

            var placableObject = part.GetComponent<PlacebleObjectBase>();
            placableObject.SetLocalFieldPosition(figure.Position);

            _objectsInFigure.Add(placableObject);
        }

        _figureHolder ??= CreateFigureHolder();
        childObjects.ForEach(o => o.transform.parent = _figureHolder.transform);
        _figureHolder.GetComponent<Figure>().Init(_objectsInFigure, color);

        HasFigure = true;
    }

    private void OnFiguresPlaced()
    {
        HasFigure = false;
        _gameEvents.InvokeGameFieldChanged();
    }

    private void OnFigureActivate()
    {
        SpawnFigure();
        HasFigure = true;
    }

    private void SpawnFigure()
    {
        if (HasFigure)
        {
            ClearExistingFigure();
        }

        Init(_figureProvider.GetNextFigure());
    }

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

    private GameObject InstansiateFigurePart(UnityEngine.Color color)
    {
        var pos = transform.position;
        var part = _placebleObjectsProvider.GetRandomPlacableObject();

        part.transform.position = new Vector3(pos.x, pos.y, pos.z);
        part.GetComponentInChildren<SpriteRenderer>().material.color = color;

        return part;
    }

    private void ClearExistingFigure()
    {
        while (_figureHolder.transform.childCount > 0)
        {
            DestroyImmediate(_figureHolder.transform.GetChild(0));
        }
    }

    private bool _canPlaceFigure;
    private List<PlacebleObjectBase> _objectsInFigure = new();
    private FigureProvider _figureProvider;
    private ColorProvider _colorProvider;
    private GameObject _figureHolder;
    private GameEvents _gameEvents;
    private PlacebleObjectsProvider _placebleObjectsProvider;
    private Tracer _tracer;
    private ScoresOnLevel _score;
    private GameObject _collider;
}