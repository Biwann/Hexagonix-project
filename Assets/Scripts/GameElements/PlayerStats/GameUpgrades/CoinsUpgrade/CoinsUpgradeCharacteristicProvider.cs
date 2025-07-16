public sealed class CoinsUpgradeCharacteristicProvider : ICharacteristicProvider
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

    public NextUpgradeType GetNextUpgrade(out int value)
    {
        value = 0;

        var (nextCoinChance, nextCoinsPerOne) = GetCharacteristics(_upgradeInformation.UpgradeLevel + 1);

        if (nextCoinChance > HexagonWithCoinChance)
        {
            value = nextCoinChance - HexagonWithCoinChance;
            return NextUpgradeType.First;
        }
        else
        {
            value = nextCoinsPerOne - CoinsPerOneHexagonWithCoin;
            return NextUpgradeType.Second;
        }
    }

    private void UpdateLocals()
    {
        var currentLevel = _upgradeInformation.UpgradeLevel;

        var (coinChance, coinsPerOne) = GetCharacteristics(currentLevel);

        HexagonWithCoinChance = coinChance;
        CoinsPerOneHexagonWithCoin = coinsPerOne;
    }

    private (int, int) GetCharacteristics(int currentLevel)
    {
        var coinChance = 0;
        var coinsPerOne = 1;

        for (int i = 0; i <= currentLevel; i++)
        {
            if (i == 0)
            {
                coinChance += 2 * PlacebleObjectsProvider.OnePercentValueCost;
            }
            else if (i <= 4) // + 4 процентов
            {
                coinChance += PlacebleObjectsProvider.OnePercentValueCost;
            }
            else if (i == 5) // + 1 монета
            {
                coinsPerOne += 1;
            }
            else if (i <= 10) // + 10 процентов
            {
                coinChance += PlacebleObjectsProvider.OnePercentValueCost * 2;
            }
            else if (i < 20) // итого 10 монет
            {
                coinsPerOne += 1;
            }
            else
            {
                coinsPerOne += 5;
            }
        }

        return (coinChance, coinsPerOne);
    }

    private readonly CoinsUpgradeInformationLocal _upgradeInformation;
}