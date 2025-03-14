using System.Collections.Generic;

public sealed class CoinsUpgradeConfig
{
    public CoinsUpgradeConfig()
    {
        _costList = new();
        CalculateCostList();
    }

    public int MaxLevel => 20;
    public int MinPlayerLevelToUse => 0;
    public int GetFeatureCost(int newLevel)
    {
        return _costList[newLevel];
    }

    private void CalculateCostList()
    {
        _costList.Add(0);
        _costList.Add(1);
        _costList.Add(2);
        for (int i = 3; i <= MaxLevel; i++)
        {
            _costList.Add(_costList[i-1] + _costList[i - 2]);
        }
    }

    private List<int> _costList; 
}