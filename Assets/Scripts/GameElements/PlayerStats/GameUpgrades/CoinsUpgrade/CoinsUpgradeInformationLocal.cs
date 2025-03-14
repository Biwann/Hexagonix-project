using System;

public sealed class CoinsUpgradeInformationLocal : IUpgradeInformation
{
    public CoinsUpgradeInformationLocal(
        CoinsUpgradeConfig config)
    {
        _config = config;
        UpgradeLevel = 0;
    }

    public event Action OnInformationChanged;

    public int UpgradeLevel
    {
        get
        {
            return _upgradeLevel;
        }
        private set
        {
            if (value <= _upgradeLevel)
            {
                return;
            }

            _upgradeLevel = value;
            OnInformationChanged?.Invoke();
        }
    }

    public int UpgradeCost => _config.GetFeatureCost(UpgradeLevel);
    
    public int MaxUpgradeLevel => _config.MaxLevel;
    
    public int MinPlayerLevelToUse => _config.MinPlayerLevelToUse;

    public bool IsAvailable => true;

    private int _upgradeLevel;
    private readonly CoinsUpgradeConfig _config;

    public void DoUpgrade()
    {
        UpgradeLevel++;
    }
}