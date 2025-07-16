using System;
using UnityEngine;

public sealed class BombsUpgradeSaverDefault : IBombsUpgradeSaver
{
    public BombsUpgradeSaverDefault()
    {
        SavedBombsUpgradeLevel = PlayerPrefs.GetInt(GameConstants.SavingBombUpgradeName, 0);
    }

    public int SavedBombsUpgradeLevel
    {
        get { return _savedBombsUpgradeLevel; }
        set
        {
            if (_savedBombsUpgradeLevel == value)
            {
                return;
            }

            _savedBombsUpgradeLevel = value;
            SavedBombsUpgradeChanged?.Invoke(_savedBombsUpgradeLevel);
        }
    }

    public event Action<int> SavedBombsUpgradeChanged;

    public bool TryChangeSavedBombsUpgradeLevevl(int level)
    {
        SavedBombsUpgradeLevel = level;
        PlayerPrefs.SetInt(GameConstants.SavingBombUpgradeName, SavedBombsUpgradeLevel);
        PlayerPrefs.Save();

        return true;
    }

    private int _savedBombsUpgradeLevel;
}