public sealed class CoinsUpgradeCharacteristicProvider
{
    public CoinsUpgradeCharacteristicProvider(
        CoinsUpgradeInformationLocal upgradeInformation)
    {
        _upgradeInformation = upgradeInformation;

        UpdateLocals();
        _upgradeInformation.OnInformationChanged += UpdateLocals;
    }

    public int HexagonWithCoinChance { get; private set; }

    public int CoinsPerOneHexagonWithCoin { get; private set; }

    private void UpdateLocals()
    {
        var currentLevel = _upgradeInformation.UpgradeLevel;

        var coinChance = 0;
        var coinsPerOne = 1;

        for (int i = 0; i <= currentLevel; i++)
        {
            if (i == 0)
            {
                continue;
            }
            else if (i <= 10)
            {
                coinChance += 10;
            }
            else if (i < 20)
            {
                coinsPerOne += 1;
            }
            else
            {
                coinsPerOne += 5;
            }
        }

        HexagonWithCoinChance = coinChance;
        CoinsPerOneHexagonWithCoin = coinsPerOne;
    }

    private readonly CoinsUpgradeInformationLocal _upgradeInformation;
}