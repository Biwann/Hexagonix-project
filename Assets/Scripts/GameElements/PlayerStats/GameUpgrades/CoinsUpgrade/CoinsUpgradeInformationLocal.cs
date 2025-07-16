public sealed class CoinsUpgradeInformationLocal : BaseUpgradeInformation
{
    public CoinsUpgradeInformationLocal(
        CoinsUpgradeConfig config,
        ExpirienceLevelProvider levelProvider) 
        : base(config, levelProvider)
    {
    }
}