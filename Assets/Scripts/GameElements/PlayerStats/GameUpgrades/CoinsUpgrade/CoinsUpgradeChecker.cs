public sealed class CoinsUpgradeChecker
{
    public CoinsUpgradeChecker(
        ICoinsUpgradeSaver coinsUpgradeSaver,
        CoinsUpgradeInformationLocal coinsLocal)
    {
        _coinsUpgradeSaver = coinsUpgradeSaver;
        _coinsUpgradeLocal = coinsLocal;

        var diff = _coinsUpgradeSaver.SavedCoinsUpgradeLevel - _coinsUpgradeLocal.UpgradeLevel;

        for (int i = 0; i < diff; i++)
        {
            _coinsUpgradeLocal.DoUpgrade();
        }

        _coinsUpgradeLocal.OnInformationChanged += OnLocalCoinsUpgradeLevelChanged;
    }

    private void OnLocalCoinsUpgradeLevelChanged()
    {
        var newLevel = _coinsUpgradeLocal.UpgradeLevel;
        _coinsUpgradeSaver.TryChangeSavedCoinsUpgradeLevevl(newLevel);
    }

    private readonly ICoinsUpgradeSaver _coinsUpgradeSaver;
    private readonly CoinsUpgradeInformationLocal _coinsUpgradeLocal;
}