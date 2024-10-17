using UnityEngine;
using Zenject;

public class GlobalProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("Global bindings");
        Container.Bind<Tracer>().FromInstance(new Tracer(Debug.Log, Debug.LogWarning)).AsSingle().NonLazy();
        Container.Bind<GlobalProgramEvents>().AsSingle().NonLazy();
#if UNITY_EDITOR
        Container.Bind<IScoresRecord>().To<ScoresRecordDefault>().AsSingle().NonLazy();
#else
        // TODO: make realisation for real game (for example take web record)
        Container.Bind<IScoresRecord>().To<ScoresRecordDefault>().AsSingle().NonLazy();
#endif
    }
}
