public sealed class ScoresUpgradeCharacteristicProvider : ICharacteristicProvider
{
    public ScoresUpgradeCharacteristicProvider(
        ScoresUpgradeInformationLocal upgradeInformation)
    {
        _upgradeInformation = upgradeInformation;

        UpdateLocals();
        _upgradeInformation.OnInformationChanged += UpdateLocals;
    }

    public int ScoresInHexagon { get; private set; }

    public NextUpgradeType GetNextUpgrade(out int value)
    {
        value = 0;

        var nextScores = GetCharacteristics(_upgradeInformation.UpgradeLevel + 1);

        value = (nextScores - ScoresInHexagon);
        return NextUpgradeType.First;
    }

    private void UpdateLocals()
    {
        var currentLevel = _upgradeInformation.UpgradeLevel;

        var scores = GetCharacteristics(currentLevel);

        ScoresInHexagon = scores;
    }

    private int GetCharacteristics(int currentLevel)
    {
        var scores = 0;

        for (int i = 0; i <= currentLevel; i++)
        {
            if (i == 0)
            {
                scores += 10;
            }
            else if (i <= 10)
            {
                scores += 1;
            }
            else if (i <= 15)
            {
                scores += 2;
            }
            else if (i <= 20)
            {
                scores += 4;
            }
            else
            {
                scores += 5;
            }
        }

        return scores;
    }

    private readonly ScoresUpgradeInformationLocal _upgradeInformation;
}