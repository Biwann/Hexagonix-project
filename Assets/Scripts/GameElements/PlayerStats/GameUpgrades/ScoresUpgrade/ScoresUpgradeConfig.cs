public sealed class ScoresUpgradeConfig : UpgradeConfig
{
    public ScoresUpgradeConfig() : base()
    {
    }

    public override int MaxLevel => 30;

    public override int MinPlayerLevelToUse => 10;

    protected override void CalculateCostList()
    {
        var shift = 10;
        var current = 20;
        for (int i = 0; i <= MaxLevel; i++)
        {
            current += shift;

            if (i <= 10)
            {
                shift += 2 * i;
            }
            else if (i <= 20)
            {
                shift += 4 * i;
            }
            else
            {
                shift += 8 * i;
            }

            _costList.Add(current);
        }
    }
}