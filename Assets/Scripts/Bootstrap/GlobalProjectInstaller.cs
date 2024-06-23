using UnityEngine;
using Zenject;

public class GlobalProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("Global bindings");
        Container.Bind<Tracer>().FromInstance(new Tracer(Debug.Log, Debug.LogWarning)).AsSingle().NonLazy();
    }
}
