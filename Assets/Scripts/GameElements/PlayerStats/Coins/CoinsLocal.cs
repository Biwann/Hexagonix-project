using System;

public sealed class CoinsLocal
{
    public CoinsLocal()
    {
        _coins = 0;
    }

    public event Action<int> CoinsChanged;

    public int Coins
    {
        get
        {
            return _coins;
        }

        private set
        {
            if (_coins == value)
            {
                return;
            }

            _coins = value;
            CoinsChanged?.Invoke(_coins);
        }
    }

    public void AddCoins(int add)
    {
        Coins += add;
    }

    public bool CanBuy(int cost)
        => _coins >= cost;

    public bool TrySpendCoins(int spend)
    {
        if (!CanBuy(spend))
        {
            return false;
        }

        Coins -= spend;
        return true;
    }

    private int _coins;
}
