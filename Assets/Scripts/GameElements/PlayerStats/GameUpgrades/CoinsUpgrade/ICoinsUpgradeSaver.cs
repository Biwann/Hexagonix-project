using System;

public interface ICoinsUpgradeSaver
{
    event Action<int> SavedCoinsUpgradeChanged;

    int SavedCoinsUpgradeLevel { get; }

    bool TryChangeSavedCoinsUpgradeLevevl(int level);
}