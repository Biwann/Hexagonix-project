using System;
using System.Data;
using UnityEngine;

public sealed class ExpirienceSaverDefault : IExpirienceSaver
{
    public ExpirienceSaverDefault()
    {
        SavedExpirience = PlayerPrefs.GetInt(GameConstants.SavingExpirienceName, defaultValue: 0);
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
        PlayerPrefs.SetInt(GameConstants.SavingExpirienceName, SavedExpirience);
        PlayerPrefs.Save();

        return true;
    }

    private int _savedExpirience;
}
