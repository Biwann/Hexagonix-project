using System;

public sealed class GameEvents
{
    public static event Action FigurePlaced;
    public static event Action NoMovesLeft;
    public static event Action GameEnded;
    public static event Action LevelChanging;

    public static void RestartLevel()
    {
        LevelChanging?.Invoke();
        GlobalProgramEvents.RestartLevel();
    }

    public static void NavigateOnMainMenu()
    {
        LevelChanging?.Invoke();
        GlobalProgramEvents.NavigateOnMainMenu();
    }

    public void InvokeFigurePlaced()
    {
        FigurePlaced?.Invoke();
    }
    
    public void InvokeNoMovesLeft()
    {
        NoMovesLeft?.Invoke();
    }

    public void InvokeGameEnd()
    { 
        GameEnded?.Invoke();
    }
}