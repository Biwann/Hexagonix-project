using System;
using UnityEngine;

public class CoinsSaverDefault : ICoinsSaver
{
    public CoinsSaverDefault()
    {
        SavedCoins = PlayerPrefs.GetInt(GameConstants.SavingCoinsName, defaultValue: 0);
    }

    public event Action<int> SavedCoinsChanged;

    public int SavedCoins
    {
        get
        {
            return _savedCoins;
        }
        private set
        {
            if ( _savedCoins == value ) 
            {
                return;
            }

            _savedCoins = value;
            SavedCoinsChanged?.Invoke(_savedCoins);
        }
    }

    public bool TryChangeSavedCoins(int coins)
    {
        SavedCoins = coins;
        PlayerPrefs.SetInt(GameConstants.SavingCoinsName, SavedCoins);
        PlayerPrefs.Save();

        return true;
    }

    private int _savedCoins;
}
