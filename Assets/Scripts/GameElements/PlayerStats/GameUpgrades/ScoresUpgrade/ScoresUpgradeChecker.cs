public sealed class ScoresUpgradeChecker
{
    public ScoresUpgradeChecker(
        IScoresUpgradeSaver bombsUpgradeSaver,
        ScoresUpgradeInformationLocal bombsLocal)
    {
        _bombsUpgradeSaver = bombsUpgradeSaver;
        _bombsUpgradeLocal = bombsLocal;

        var diff = _bombsUpgradeSaver.SavedScoresUpgradeLevel - _bombsUpgradeLocal.UpgradeLevel;

        for (int i = 0; i < diff; i++)
        {
            _bombsUpgradeLocal.DoUpgrade();
        }

        _bombsUpgradeLocal.OnInformationChanged += OnLocalScoresUpgradeLevelChanged;
    }

    private void OnLocalScoresUpgradeLevelChanged()
    {
        var newLevel = _bombsUpgradeLocal.UpgradeLevel;
        _bombsUpgradeSaver.TryChangeSavedScoresUpgradeLevevl(newLevel);
    }

    private readonly IScoresUpgradeSaver _bombsUpgradeSaver;
    private readonly ScoresUpgradeInformationLocal _bombsUpgradeLocal;
}