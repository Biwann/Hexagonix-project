using UnityEngine;
using Zenject;

public sealed class PlacableObjectsFactory
{
    public PlacableObjectsFactory(
        DiContainer container,
        UnityObjectLifeController unityObjecController,
        PrefabLoader prefabLoader) 
    {
        _container = container;
        _unityObjecController = unityObjecController;
        _prefabLoader = prefabLoader;
    }

    public GameObject GetPlacableObject(PlacableObjectType type)
    {
        return type switch
        {
            PlacableObjectType.DefaultHexagon => CreateDefaultHexagon(),
            PlacableObjectType.HexagonWithCoin => CreateHexagonWithCoin(),
            PlacableObjectType.HexagonWithBomb => CreateHexagonWithBomb(),
            _ => null
        };
    }

    private GameObject CreateDefaultHexagon()
    {
        var gameObject = CreateInstance(_prefabLoader.DefaultHexagon);

        return gameObject;
    }

    private GameObject CreateHexagonWithCoin()
    {
        var gameObject = CreateInstance(_prefabLoader.HexagonWithCoin);

        var compromnent = gameObject.GetComponent<HexagonWithCoin>();
        compromnent.Init(
            _container.Resolve<CoinsLocal>(),
            _container.Resolve<CoinsUpgradeCharacteristicProvider>());

        return gameObject;
    }

    private GameObject CreateHexagonWithBomb()
    {
        var gameObject = CreateInstance(_prefabLoader.HexagonWithBomb);

        var compromnent = gameObject.GetComponent<HexagonWithBomb>();
        compromnent.Init(
            _container.Resolve<CellFolder>(),
            _container.Resolve<TextAnimation>(),
            _container.Resolve<ScoresOnLevel>(),
            _container.Resolve<GameEvents>());

        return gameObject;
    }

    private GameObject CreateInstance(GameObject prefab)
        => _unityObjecController.Instantiate(prefab, Vector3.zero, Quaternion.identity);

    private readonly DiContainer _container;
    private readonly UnityObjectLifeController _unityObjecController;
    private readonly PrefabLoader _prefabLoader;
}