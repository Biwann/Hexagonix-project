using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class PlacebleObjectsProvider
{
    public const int OnePercentValueCost = 10;

    public PlacebleObjectsProvider(
        PlacableObjectsFactory objectsFactory,
        CoinsUpgradeCharacteristicProvider coinsUpgradeCharacteristic,
        BombsUpgradeCharacteristicProvider bombsUpgradeCharacteristic,
        Tracer tracer)
    {
        _objectsFactory = objectsFactory;
        _hexagonWithCoinChance = coinsUpgradeCharacteristic.HexagonWithCoinChance;
        _hexagonWithBombChance = bombsUpgradeCharacteristic.HexagonWithBombChance;
        _random = new System.Random();
        _tracer = tracer;

        _tracer.TraceDebug($"coin chance = {(float)_hexagonWithCoinChance / OnePercentValueCost}" +
            $"; bomb chance = {(float)_hexagonWithBombChance / OnePercentValueCost}");

        _defaultHexagonChance = MaxChance - _hexagonWithCoinChance - _hexagonWithBombChance;

        _placebleObjectTypeAndChanceValueList = new()
        {
            (PlacableObjectType.DefaultHexagon, () => _defaultHexagonChance),
            (PlacableObjectType.HexagonWithCoin, () => _hexagonWithCoinChance),
            (PlacableObjectType.HexagonWithBomb, () => _hexagonWithBombChance),
        };


        _maxCoinSkips = 2 * MaxChance / (_hexagonWithCoinChance == 0 ? 1 : _hexagonWithCoinChance);
        _maxBombSkips = 2 * MaxChance / (_hexagonWithBombChance == 0 ? 1 : _hexagonWithBombChance);
    }

    public GameObject GetRandomPlacableObject()
    {
        if (ShouldForceGiveType(out var forcedType))
        {
            return GetPlacableObject(forcedType);
        }

        var weightsSum = _placebleObjectTypeAndChanceValueList.Sum(t => t.getChanceValue());
        var randomInt = _random.Next(weightsSum);

        var accumulated = 0;
        foreach (var (type, chanceGetter) in _placebleObjectTypeAndChanceValueList)
        {
            var chance = chanceGetter();
            accumulated += chance;

            if (randomInt <= accumulated)
            {
                return GetPlacableObject(type);
            }
        }

        throw new ArgumentOutOfRangeException($"weight sum: {weightsSum}, random int: {randomInt}, accumulated: {accumulated}");
    }

    private bool ShouldForceGiveType(out PlacableObjectType type)
    {
        type = PlacableObjectType.Unknown;
        var result = false;

        if (_coinSkips >= _maxCoinSkips && _hexagonWithCoinChance != 0)
        {
            type = PlacableObjectType.HexagonWithCoin;
            result = true;
        } 
        else if (_bombSkips >= _maxBombSkips && _hexagonWithBombChance != 0)
        {
            type = PlacableObjectType.HexagonWithBomb;
            result = true;
        }

        if (result)
        {
            _tracer.TraceDebug($"force figure part {type}");
        }

        return result;
    }

    private GameObject GetPlacableObject(PlacableObjectType type)
    {
        _coinSkips++;
        _bombSkips++;

        if (type == PlacableObjectType.HexagonWithCoin)
            _coinSkips = 0;
        if (type == PlacableObjectType.HexagonWithBomb)
            _bombSkips = 0;

        return _objectsFactory.GetPlacableObject(type);
    }

    private int _coinSkips;
    private readonly int _maxCoinSkips;
    private int _bombSkips;
    private readonly int _maxBombSkips;

    private const int MaxChance = 100 * OnePercentValueCost;
    private readonly int _hexagonWithCoinChance;
    private readonly int _hexagonWithBombChance;
    private readonly int _defaultHexagonChance;
    private readonly PlacableObjectsFactory _objectsFactory;
    private readonly System.Random _random;
    private readonly Tracer _tracer;
    private readonly List<(PlacableObjectType getFigureType, Func<int> getChanceValue)> _placebleObjectTypeAndChanceValueList;
}