using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindSingle<GameEvents>();
        BindSingle<CellFolder>();
        BindSingle<HexagonixFieldProvider>();
        BindSingle<ColorProvider>();
        BindSingle<FiguresCollection>();
        BindSingle<ColumnDestroyer>();
        BindSingle<FigureProvider>();
        BindSingle<FiguresManager>();

        BindSingle<ScoresOnLevel>();
        BindSingle<RecordChecker>();
        BindSingle<AddExpirienceOnScoreTracker>();

        BindSingle<NoMovesLeft>();

        Container.Bind<UnityObjectLifeController>()
            .FromInstance(new(Instantiate, Destroy, StartCoroutine))
            .AsSingle().NonLazy();
    }

    private void BindSingle<T>()
    {
        Container.Bind<T>().AsSingle().NonLazy();
    }
}
