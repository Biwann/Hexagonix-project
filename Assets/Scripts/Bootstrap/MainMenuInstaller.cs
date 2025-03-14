using UnityEngine;
using Zenject;

public sealed class MainMenuInstaller : MonoInstaller
{
    [SerializeField] UpgradeView _coinsUpgradeView;

    public override void InstallBindings()
    {
        _coinsUpgradeView.Inject(
            Container.Resolve<CoinsUpgradeInformationLocal>(),
            Container.Resolve<CoinsLocal>());
        Debug.Log("Main menu bindings");
    }
}