
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("Scene bindings");
        Container.Bind<Tracer>().FromInstance(new Tracer(Debug.Log, Debug.LogWarning)).AsSingle().NonLazy();
        Container.Bind<CellFolder>().AsSingle().NonLazy();
    }
}
