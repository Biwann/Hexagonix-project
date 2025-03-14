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

        BindCoinsUpgrade();
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
        BindSingle<ExpirienceLocal>();
        BindSingle<ExpirienceLevelProvider>();
        BindSingle<ExpirienceChecker>();

        Container.Bind<IExpirienceSaver>()
#if UNITY_EDITOR
            .To<ExpirienceSaverDefault>()
#else
            .To<ExpirienceSaverDefault>()
#endif
            .AsSingle().NonLazy();
    }

    private void BindCoinsSystem()
    {
        BindSingle<CoinsLocal>();
        BindSingle<CoinsChecker>();

        Container.Bind<ICoinsSaver>()
#if UNITY_EDITOR
            .To<CoinsSaverDefault>()
#else
            .To<CoinsSaverDefault>()
#endif
            .AsSingle().NonLazy();
    }

    private void BindCoinsUpgrade()
    {
        Container.Bind<ICoinsUpgradeSaver>()
#if UNITY_EDITOR
            .To<CoinsUpgradeSaverDefault>()
#else
            // TODO: make product realization
            .To<CoinsUpgradeSaverDefault>()
#endif
            .AsSingle().NonLazy();

        BindSingle<CoinsUpgradeConfig>();
        BindSingle<CoinsUpgradeInformationLocal>();
        BindSingle<CoinsUpgradeChecker>();
        BindSingle<CoinsUpgradeCharacteristicProvider>();
    }

    private void BindSingle<T>()
    {
        Container.Bind<T>().AsSingle().NonLazy();
    }
}
