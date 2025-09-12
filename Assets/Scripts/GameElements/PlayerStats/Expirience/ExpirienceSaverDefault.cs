using System;
using System.Data;
using UnityEngine;

public sealed class ExpirienceSaverDefault : IExpirienceSaver
{
    public ExpirienceSaverDefault(IDataSaver dataSaver)
    {
        _dataSaver = dataSaver;
        SavedExpirience = _dataSaver.GetInt(GameConstants.SavingExpirienceName, defaultValue: 0);
    }

    public event Action<int> SavedExpirienceChanged;

    public int SavedExpirience
    {
        get
        {
            return _savedExpirience;
        }
        private set
        {
            if (value == _savedExpirience)
            {
                return;
            }

            _savedExpirience = value;
            SavedExpirienceChanged?.Invoke(_savedExpirience);
        }
    }

    public bool TryChangeSavedExpirience(int exp)
    {
        if (exp <= SavedExpirience)
        {
            return false;
        }

        SavedExpirience = exp;
        _dataSaver.SetInt(GameConstants.SavingExpirienceName, SavedExpirience);

        return true;
    }

    private readonly IDataSaver _dataSaver;
    private int _savedExpirience;
}
