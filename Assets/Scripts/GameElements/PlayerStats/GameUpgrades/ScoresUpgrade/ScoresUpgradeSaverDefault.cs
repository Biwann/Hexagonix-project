using System;
using UnityEngine;

public sealed class ScoresUpgradeSaverDefault : IScoresUpgradeSaver
{
    public ScoresUpgradeSaverDefault(IDataSaver dataSaver)
    {
        _dataSaver = dataSaver;
        SavedScoresUpgradeLevel = _dataSaver.GetInt(GameConstants.SavingScoresUpgradeName, 0);
    }

    public int SavedScoresUpgradeLevel
    {
        get { return _savedScoresUpgradeLevel; }
        set
        {
            if (_savedScoresUpgradeLevel == value)
            {
                return;
            }

            _savedScoresUpgradeLevel = value;
            SavedScoresUpgradeChanged?.Invoke(_savedScoresUpgradeLevel);
        }
    }

    public event Action<int> SavedScoresUpgradeChanged;

    public bool TryChangeSavedScoresUpgradeLevevl(int level)
    {
        SavedScoresUpgradeLevel = level;
        _dataSaver.SetInt(GameConstants.SavingScoresUpgradeName, SavedScoresUpgradeLevel);

        return true;
    }

    private readonly IDataSaver _dataSaver;
    private int _savedScoresUpgradeLevel;
}