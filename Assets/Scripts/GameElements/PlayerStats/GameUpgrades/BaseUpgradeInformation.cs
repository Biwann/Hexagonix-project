using System;

public abstract class BaseUpgradeInformation : IUpgradeInformation
{
    protected BaseUpgradeInformation(
        UpgradeConfig config,
        ExpirienceLevelProvider playerLevel)
    {
        Config = config;
        PlayerLevel = playerLevel;
        UpgradeLevel = 0;
    }

    public event Action OnInformationChanged;

    public int UpgradeCost => Config.GetFeatureCost(UpgradeLevel);

    public int UpgradeLevel
    {
        get
        {
            return _upgradeLevel;
        }
        protected set
        {
            if (value <= _upgradeLevel)
            {
                return;
            }

            _upgradeLevel = value;
            RaiseInformationChanged();
        }
    }


    public int MaxUpgradeLevel => Config.MaxLevel;

    public int MinPlayerLevelToUse => Config.MinPlayerLevelToUse;

    public virtual bool IsAvailable => PlayerLevel.CurrentLevelInformation.Level >= MinPlayerLevelToUse;

    public void DoUpgrade()
    {
        UpgradeLevel += 1;
    }

    protected void RaiseInformationChanged() => OnInformationChanged?.Invoke();

    protected readonly UpgradeConfig Config;
    protected readonly ExpirienceLevelProvider PlayerLevel;

    private int _upgradeLevel;
}