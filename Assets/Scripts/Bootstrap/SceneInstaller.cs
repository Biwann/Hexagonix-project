using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindGameField();
        BindFugures();
        BindStats();

        BindSingle<NoMovesLeft>();

        Debug.Log("Scene bindings");
    }

    private void BindGameField()
    {
        BindSingle<GameEvents>();
        BindSingle<CellFolder>();
        BindSingle<HexagonixFieldProvider>();
    }

    private void BindFugures()
    {
        BindSingle<ColorProvider>();
        BindSingle<FiguresCollection>();
        BindSingle<ColumnDestroyer>();
        BindSingle<FigureProvider>();
        BindSingle<FiguresManager>();
        BindSingle<PlacableObjectsFactory>();
        BindSingle<PlacebleObjectsProvider>();
    }

    private void BindStats()
    {
        BindSingle<ScoresOnLevel>();
        BindSingle<RecordChecker>();
        BindSingle<AddExpirienceOnScoreTracker>();
    }

    private void BindSingle<T>()
    {
        Container.Bind<T>().AsSingle().NonLazy();
    }
}
