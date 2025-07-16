using System;

public interface IScoresUpgradeSaver
{
    event Action<int> SavedScoresUpgradeChanged;

    int SavedScoresUpgradeLevel { get; }

    bool TryChangeSavedScoresUpgradeLevevl(int level);
}