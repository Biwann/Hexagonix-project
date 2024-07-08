using System;

public sealed class GameEvents
{
    public event Action PlacedOnField;

    public void InvokePlacedOnField()
    {
        PlacedOnField?.Invoke();
    }
}