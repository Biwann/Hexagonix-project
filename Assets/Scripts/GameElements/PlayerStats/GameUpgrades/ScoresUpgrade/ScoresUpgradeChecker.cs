public sealed class ScoresUpgradeChecker
{
    public ScoresUpgradeChecker(
        IScoresUpgradeSaver bombsUpgradeSaver,
        ScoresUpgradeInformationLocal bombsLocal)
    {
        _scoresUpgradeSaver = bombsUpgradeSaver;
        _scoresUpgradeLocal = bombsLocal;

        var diff = _scoresUpgradeSaver.SavedScoresUpgradeLevel - _scoresUpgradeLocal.UpgradeLevel;

        for (int i = 0; i < diff; i++)
        {
            _scoresUpgradeLocal.DoUpgrade();
        }

        _scoresUpgradeLocal.OnInformationChanged += OnLocalScoresUpgradeLevelChanged;
    }

    private void OnLocalScoresUpgradeLevelChanged()
    {
        var newLevel = _scoresUpgradeLocal.UpgradeLevel;
        _scoresUpgradeSaver.TryChangeSavedScoresUpgradeLevevl(newLevel);
    }

    private readonly IScoresUpgradeSaver _scoresUpgradeSaver;
    private readonly ScoresUpgradeInformationLocal _scoresUpgradeLocal;
}