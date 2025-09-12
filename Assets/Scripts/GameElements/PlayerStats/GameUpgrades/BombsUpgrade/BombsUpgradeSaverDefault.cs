using System;
using UnityEngine;

public sealed class BombsUpgradeSaverDefault : IBombsUpgradeSaver
{
    public BombsUpgradeSaverDefault(IDataSaver dataSaver)
    {
        _dataSaver = dataSaver;
        SavedBombsUpgradeLevel = _dataSaver.GetInt(GameConstants.SavingBombUpgradeName, 0);
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
        _dataSaver.SetInt(GameConstants.SavingBombUpgradeName, SavedBombsUpgradeLevel);

        return true;
    }

    private readonly IDataSaver _dataSaver;
    private int _savedBombsUpgradeLevel;
}