using System;
using UnityEngine;

public class CoinsSaverDefault : ICoinsSaver
{
    public CoinsSaverDefault(IDataSaver dataSaver)
    {
        _dataSaver = dataSaver;
        SavedCoins = _dataSaver.GetInt(GameConstants.SavingCoinsName, defaultValue: 0);
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
        _dataSaver.SetInt(GameConstants.SavingCoinsName, SavedCoins);

        return true;
    }

    private readonly IDataSaver _dataSaver;
    private int _savedCoins;
}
