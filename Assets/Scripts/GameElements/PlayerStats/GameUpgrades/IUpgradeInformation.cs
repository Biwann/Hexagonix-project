using System;

public interface IUpgradeInformation
{
    public event Action OnInformationChanged;
    public int UpgradeCost { get; }
    public int UpgradeLevel { get; }
    public int MaxUpgradeLevel { get; }
    public int MinPlayerLevelToUse { get; }
    public bool IsAvailable { get; }
    public void DoUpgrade();
}