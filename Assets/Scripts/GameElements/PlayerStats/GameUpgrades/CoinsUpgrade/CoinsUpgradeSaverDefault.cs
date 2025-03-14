using System;
using UnityEngine;

public sealed class CoinsUpgradeSaverDefault : ICoinsUpgradeSaver
{
    public CoinsUpgradeSaverDefault()
    {
        SavedCoinsUpgradeLevel = 0;//PlayerPrefs.GetInt(GameConstants.SavingCoinsUpgradeName, 0);
    }

    public int SavedCoinsUpgradeLevel
    {
        get { return _savedCoinsUpgradeLevel; }
        set 
        { 
            if (_savedCoinsUpgradeLevel == value) 
            {
                return;
            }

            _savedCoinsUpgradeLevel = value;
            SavedCoinsUpgradeChanged?.Invoke(_savedCoinsUpgradeLevel);
        }
    }

    public event Action<int> SavedCoinsUpgradeChanged;

    public bool TryChangeSavedCoinsUpgradeLevevl(int level)
    {
        SavedCoinsUpgradeLevel = level;
        PlayerPrefs.SetInt(GameConstants.SavingCoinsUpgradeName, SavedCoinsUpgradeLevel);
        PlayerPrefs.Save();

        return true;
    }

    private int _savedCoinsUpgradeLevel;
}
