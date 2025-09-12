using System;
using UnityEngine;

public sealed class CoinsUpgradeSaverDefault : ICoinsUpgradeSaver
{
    public CoinsUpgradeSaverDefault(IDataSaver dataSaver)
    {
        _dataSaver = dataSaver;
        SavedCoinsUpgradeLevel = _dataSaver.GetInt(GameConstants.SavingCoinsUpgradeName, 0);
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
        _dataSaver.SetInt(GameConstants.SavingCoinsUpgradeName, SavedCoinsUpgradeLevel);

        return true;
    }

    private readonly IDataSaver _dataSaver;
    private int _savedCoinsUpgradeLevel;
}
