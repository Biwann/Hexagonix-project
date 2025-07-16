public sealed class BombsUpgradeCharacteristicProvider : ICharacteristicProvider
{
    public BombsUpgradeCharacteristicProvider(
        BombsUpgradeInformationLocal upgradeInformation)
    {
        _upgradeInformation = upgradeInformation;

        UpdateLocals();
        _upgradeInformation.OnInformationChanged += UpdateLocals;
    }

    public int HexagonWithBombChance { get; private set; }

    public NextUpgradeType GetNextUpgrade(out int value)
    {
        value = 0;

        var nextBombChance = GetCharacteristics(_upgradeInformation.UpgradeLevel + 1);

        value = (nextBombChance - HexagonWithBombChance);
        return NextUpgradeType.First;
    }

    private void UpdateLocals()
    {
        var currentLevel = _upgradeInformation.UpgradeLevel;

        var bombChance = GetCharacteristics(currentLevel);

        HexagonWithBombChance = bombChance;
    }

    private int GetCharacteristics(int currentLevel)
    {
        var bombChance = 0;

        for (int i = 0; i <= currentLevel; i++)
        {
            if (i == 0)
            {
                continue;
            }
            else if (i <= 20) // 10 процентов
            {
                bombChance += PlacebleObjectsProvider.OnePercentValueCost / 2;
            }
            else if (i <= 25) // итого 15 процентов
            {
                bombChance += PlacebleObjectsProvider.OnePercentValueCost;
            }
        }

        return bombChance;
    }

    private readonly BombsUpgradeInformationLocal _upgradeInformation;
}