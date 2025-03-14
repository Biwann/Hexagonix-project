using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class PlacebleObjectsProvider
{
    public PlacebleObjectsProvider(
        PlacableObjectsFactory objectsFactory,
        CoinsUpgradeCharacteristicProvider coinsUpgradeCharacteristic)
    {
        _objectsFactory = objectsFactory;
        _hexagonWithCoinChance = coinsUpgradeCharacteristic.HexagonWithCoinChance;
        _random = new System.Random();

        _defaultHexagonChance = MaxChance - _hexagonWithCoinChance;

        _placebleObjectAndChanceValueList = new()
        {
            (() => _objectsFactory.GetPlacableObject(PlacableObjectType.DefaultHexagon), () => _defaultHexagonChance),
            (() => _objectsFactory.GetPlacableObject(PlacableObjectType.HexagonWithCoin), () => _hexagonWithCoinChance),
        };
    }

    public GameObject GetRandomPlacableObject()
    {
        var weightsSum = _placebleObjectAndChanceValueList.Sum(t => t.getChanceValue());
        var randomInt = _random.Next(weightsSum);

        var accumulated = 0;
        foreach (var (objGetter, chanceGetter) in _placebleObjectAndChanceValueList)
        {
            var chance = chanceGetter();
            accumulated += chance;

            if (randomInt <= accumulated)
            {
                return objGetter();
            }
        }

        throw new ArgumentOutOfRangeException($"weight sum: {weightsSum}, random int: {randomInt}, accumulated: {accumulated}");
    }

    private const int MaxChance = 1000;
    private readonly int _hexagonWithCoinChance;
    private readonly int _defaultHexagonChance;
    private readonly PlacableObjectsFactory _objectsFactory;
    private readonly System.Random _random;
    private readonly List<(Func<GameObject> getFigurePart, Func<int> getChanceValue)> _placebleObjectAndChanceValueList;
}