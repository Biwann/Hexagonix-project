using System;

public interface IBombsUpgradeSaver
{
    event Action<int> SavedBombsUpgradeChanged;

    int SavedBombsUpgradeLevel { get; }

    bool TryChangeSavedBombsUpgradeLevevl(int level);
}