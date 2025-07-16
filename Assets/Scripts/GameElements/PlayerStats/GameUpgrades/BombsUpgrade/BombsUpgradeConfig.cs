using DG.Tweening.Plugins;
using System.Collections.Generic;

public sealed class BombsUpgradeConfig : UpgradeConfig
{
    public BombsUpgradeConfig() : base()
    {
    }

    public override int MinPlayerLevelToUse => 5;
    public override int MaxLevel => 25;

    protected override void CalculateCostList()
    {
        var shift = 5;
        var current = 0;
        for (int i = 0; i <= MaxLevel; i++)
        {
            current += shift;

            if (i <= 10)
            {
                shift += 2 * i;
            }
            else
            {
                shift += 8 * i;
            }

            _costList.Add(current);
        }
    }
}
