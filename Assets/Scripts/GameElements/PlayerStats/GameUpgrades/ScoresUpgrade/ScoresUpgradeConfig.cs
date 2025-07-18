public sealed class ScoresUpgradeConfig : UpgradeConfig
{
    public ScoresUpgradeConfig() : base()
    {
    }

    public override int MaxLevel => 20;

    public override int MinPlayerLevelToUse => 10;

    protected override void CalculateCostList()
    {
        var shift = 20;
        var current = 0;
        for (int i = 0; i <= MaxLevel; i++)
        {
            current += shift;
            
            if (i == 0)
            { }
            else if (i <= 10)
            {
                shift += 4 * i;
            }
            else if (i <= 20)
            {
                shift += 8 * i;
            }
            else
            {
                shift += 16 * i;
            }

            _costList.Add(current);
            // всего 34 260 монет
        }
    }
}