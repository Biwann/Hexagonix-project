using UnityEngine;
using Zenject;

public class GlobalProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        GameSettings();

        BindServices();

        BindScoreSystem();
        BindExpirienceSystem();
        BindCoinsSystem();

        BindCoinsUpgrade();
        BindBombsUpgrade();
        BindScoresUpgrade();

        Debug.Log("Global bindings");
    }

    private void GameSettings()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    private void BindServices()
    {
        Container.Bind<Tracer>().FromInstance(new Tracer(Debug.Log, Debug.LogWarning)).AsSingle().NonLazy();

        BindSingle<GlobalProgramEvents>();
        BindSingle<PrefabLoader>();
        BindSingle<TextAnimation>();

        Container.Bind<UnityObjectLifeController>()
            .FromInstance(new(Instantiate, Destroy, StartCoroutine, StopCoroutine))
            .AsSingle().NonLazy();

        Container.Bind<IDataSaver>()
            .To<DataSaverDefault>()
            .AsSingle().NonLazy();
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

    private void BindBombsUpgrade()
    {
        Container.Bind<IBombsUpgradeSaver>()
#if UNITY_EDITOR
            .To<BombsUpgradeSaverDefault>()
#else
            // TODO: make product realization
            .To<BombsUpgradeSaverDefault>()
#endif
            .AsSingle().NonLazy();

        BindSingle<BombsUpgradeConfig>();
        BindSingle<BombsUpgradeInformationLocal>();
        BindSingle<BombsUpgradeChecker>();
        BindSingle<BombsUpgradeCharacteristicProvider>();
    }

    private void BindScoresUpgrade()
    {

        Container.Bind<IScoresUpgradeSaver>()
#if UNITY_EDITOR
            .To<ScoresUpgradeSaverDefault>()
#else
            // TODO: make product realization
            .To<ScoresUpgradeSaverDefault>()
#endif
            .AsSingle().NonLazy();

        BindSingle<ScoresUpgradeConfig>();
        BindSingle<ScoresUpgradeInformationLocal>();
        BindSingle<ScoresUpgradeChecker>();
        BindSingle<ScoresUpgradeCharacteristicProvider>();
    }

    private void BindSingle<T>()
    {
        Container.Bind<T>().AsSingle().NonLazy();
    }
}
