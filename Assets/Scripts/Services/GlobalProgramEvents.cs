using System;
using UnityEngine.SceneManagement;

public sealed class GlobalProgramEvents
{
    public static event Action LevelChanging;
    
    public static void RestartLevel()
        => ChangeLevel(SceneManager.GetActiveScene().name);

    public static void NavigateOnMainMenu()
        => ChangeLevel(GameConstants.MainMenuSceneName);

    public static void NavigateOnGame()
        => ChangeLevel(GameConstants.GameSceneName);
    
    private static void ChangeLevel(string sceneName)
    {
        LevelChanging?.Invoke();
        SceneManager.LoadScene(sceneName);
    }
}