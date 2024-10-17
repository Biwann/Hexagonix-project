using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameEvents>().AsSingle().NonLazy();
        Container.Bind<CellFolder>().AsSingle().NonLazy();
        Container.Bind<HexagonixFieldProvider>().AsSingle().NonLazy();
        Container.Bind<ColorProvider>().AsSingle().NonLazy();
        Container.Bind<FiguresCollection>().AsSingle().NonLazy();
        Container.Bind<ColumnDestroyer>().AsSingle().NonLazy();
        Container.Bind<FigureProvider>().AsSingle().NonLazy();
        Container.Bind<FiguresManager>().AsSingle().NonLazy();
        Container.Bind<ScoresOnLevel>().AsSingle().NonLazy();
        Container.Bind<RecordChecker>().AsSingle().NonLazy();
    }
}
