using System;

public sealed class ExpirienceLocal
{
    public ExpirienceLocal()
    {
        _expirience = 0;
    }

    public event Action<int> ExpirienceChanged;

    public int Expirience
    {
        get
        {
            return _expirience;
        }
        private set
        {
            if (_expirience == value) 
            {
                return;
            }

            _expirience = value;
            ExpirienceChanged?.Invoke(_expirience);
        }
    }

    public void AddExpirience(int add)
    {
        Expirience += add;
    }

    private int _expirience;
}