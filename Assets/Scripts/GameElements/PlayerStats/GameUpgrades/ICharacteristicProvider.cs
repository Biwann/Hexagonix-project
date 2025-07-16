public interface ICharacteristicProvider
{
    NextUpgradeType GetNextUpgrade(out int value);
}