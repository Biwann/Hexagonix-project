public sealed class BombsUpgradeChecker
{
    public BombsUpgradeChecker(
        IBombsUpgradeSaver bombsUpgradeSaver,
        BombsUpgradeInformationLocal bombsLocal)
    {
        _bombsUpgradeSaver = bombsUpgradeSaver;
        _bombsUpgradeLocal = bombsLocal;

        var diff = _bombsUpgradeSaver.SavedBombsUpgradeLevel - _bombsUpgradeLocal.UpgradeLevel;

        for (int i = 0; i < diff; i++)
        {
            _bombsUpgradeLocal.DoUpgrade();
        }

        _bombsUpgradeLocal.OnInformationChanged += OnLocalBombsUpgradeLevelChanged;
    }

    private void OnLocalBombsUpgradeLevelChanged()
    {
        var newLevel = _bombsUpgradeLocal.UpgradeLevel;
        _bombsUpgradeSaver.TryChangeSavedBombsUpgradeLevevl(newLevel);
    }

    private readonly IBombsUpgradeSaver _bombsUpgradeSaver;
    private readonly BombsUpgradeInformationLocal _bombsUpgradeLocal;
}