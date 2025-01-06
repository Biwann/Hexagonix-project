using UnityEngine;
using Zenject;

public class GlobalProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("Global bindings");

        Container.Bind<Tracer>().FromInstance(new Tracer(Debug.Log, Debug.LogWarning)).AsSingle().NonLazy();
        
        BindSingle<GlobalProgramEvents>();
        BindSingle<PrefabLoader>();
        
        BindScoreSystem();
        BindExpirienceSystem();
        BindCoinsSystem();
    }

    private void BindScoreSystem()
    {

        Container.Bind<IScoresRecord>()
#if UNITY_EDITOR
            .To<ScoresRecordDefault>()
#else
            // TODO: make realisation for real game (for example take web record)
            .To<ScoresRecordDefault>()
#endif
            .AsSingle().NonLazy();
    }

    private void BindExpirienceSystem()
    {

        Container.Bind<IExpirienceSaver>()
#if UNITY_EDITOR
            .To<ExpirienceSaverDefault>()
#else
            .To<ExpirienceSaverDefault>()
#endif
            .AsSingle().NonLazy();

        BindSingle<ExpirienceLocal>();
        BindSingle<ExpirienceLevelProvider>();
        BindSingle<ExpirienceChecker>();
    }

    private void BindCoinsSystem()
    {

    }

    private void BindSingle<T>()
    {
        Container.Bind<T>().AsSingle().NonLazy();
    }
}
