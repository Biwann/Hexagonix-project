using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("Scene bindings");
        Container.Bind<GameEvents>().AsSingle().NonLazy();
        Container.Bind<CellFolder>().AsSingle().NonLazy();
        Container.Bind<ColorProvider>().AsSingle().NonLazy();
        Container.Bind<FiguresCollection>().AsSingle().NonLazy();
        Container.Bind<FigureProvider>().AsSingle().NonLazy();
        Container.Bind<FiguresManager>().AsSingle().NonLazy();
        Container.Bind<ColumnDestroyerManager>().AsSingle().NonLazy();
        Container.Bind<ScoresOnLevel>().AsSingle().NonLazy();
    }
}
