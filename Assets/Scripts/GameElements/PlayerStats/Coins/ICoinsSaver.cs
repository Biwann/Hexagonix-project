using System;

public interface ICoinsSaver
{
    event Action<int> SavedCoinsChanged;

    int SavedCoins { get; }

    bool TryChangeSavedCoins(int coins);
}
