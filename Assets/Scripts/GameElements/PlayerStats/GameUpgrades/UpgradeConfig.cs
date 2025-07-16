using System.Collections.Generic;

public abstract class UpgradeConfig
{
    protected UpgradeConfig()
    {
        _costList = new();
        CalculateCostList();
    }

    public virtual int MaxLevel => 20;
    public virtual int MinPlayerLevelToUse => 0;

    public int GetFeatureCost(int newLevel)
    {
        return _costList[newLevel];
    }

    protected virtual void CalculateCostList()
    {
        _costList.AddRange(new List<int>
        {
            1,
            2,
            3,
        });

        for (int i = 3; i <= MaxLevel; i++)
        {
            _costList.Add(_costList[i - 1] + _costList[i - 2]);
        }
    }

    protected List<int> _costList;
}