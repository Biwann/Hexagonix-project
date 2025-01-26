using System;
using UnityEngine;

public class PrefabLoader
{
    public PrefabLoader()
    {
        _addedScoreText =   LazyLoad(@"LoadPrefabs/Text/AddedScoreText");
        _cell =             LazyLoad(@"LoadPrefabs/GameElements/Cell");
        _figure =           LazyLoad(@"LoadPrefabs/GameElements/Figure");
        _figureCreator =    LazyLoad(@"LoadPrefabs/GameElements/FigureCreator");
        _defaultHexagon =   LazyLoad(@"LoadPrefabs/PlacebleObjects/DefaultHexagon");
        _hexagonWithCoin =  LazyLoad(@"LoadPrefabs/PlacebleObjects/HexagonWithCoin");
    }

    public GameObject AddedScoreText 
    {
        get => _addedScoreText.Value;
    }
    
    public GameObject Cell 
    {
        get => _cell.Value;
    }
    
    public GameObject Figure 
    {
        get => _figure.Value;
    }
    
    public GameObject FigureCreator 
    {
        get => _figureCreator.Value;
    }
    
    public GameObject DefaultHexagon 
    {
        get => _defaultHexagon.Value;
    }

    public GameObject HexagonWithCoin
    {
        get => _hexagonWithCoin.Value;
    }

    private static Lazy<GameObject> LazyLoad(string path)
        => new Lazy<GameObject>(() => Resources.Load<GameObject>(path));

    private readonly Lazy<GameObject> _addedScoreText;
    private readonly Lazy<GameObject> _cell;
    private readonly Lazy<GameObject> _figure;
    private readonly Lazy<GameObject> _figureCreator;
    private readonly Lazy<GameObject> _defaultHexagon;
    private readonly Lazy<GameObject> _hexagonWithCoin;
}
