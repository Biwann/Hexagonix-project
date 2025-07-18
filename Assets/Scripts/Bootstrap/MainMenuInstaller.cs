using UnityEngine;
using Zenject;

public sealed class MainMenuInstaller : MonoInstaller
{
    [SerializeField] UpgradeView _coinsUpgradeView;
    [SerializeField] UpgradeView _bombsUpgradeView;
    [SerializeField] UpgradeView _scoreUpgradeView;

    public override void InstallBindings()
    {
        _coinsUpgradeView.Inject(
            Container.Resolve<CoinsUpgradeInformationLocal>(),
            Container.Resolve<CoinsUpgradeCharacteristicProvider>(),
            Container.Resolve<CoinsLocal>());

        _bombsUpgradeView.Inject(
            Container.Resolve<BombsUpgradeInformationLocal>(),
            Container.Resolve<BombsUpgradeCharacteristicProvider>(),
            Container.Resolve<CoinsLocal>());

        _scoreUpgradeView.Inject(
            Container.Resolve<ScoresUpgradeInformationLocal>(),
            Container.Resolve<ScoresUpgradeCharacteristicProvider>(),
            Container.Resolve<CoinsLocal>());

        Debug.Log("Main menu bindings");
    }
}