using System;
using UnityEngine;

public sealed class ScoresUpgradeSaverDefault : IScoresUpgradeSaver
{
    public ScoresUpgradeSaverDefault()
    {
        SavedScoresUpgradeLevel = PlayerPrefs.GetInt(GameConstants.SavingScoresUpgradeName, 0);
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
        PlayerPrefs.SetInt(GameConstants.SavingScoresUpgradeName, SavedScoresUpgradeLevel);
        PlayerPrefs.Save();

        return true;
    }

    private int _savedScoresUpgradeLevel;
}