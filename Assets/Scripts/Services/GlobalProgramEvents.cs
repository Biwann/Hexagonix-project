using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GlobalProgramEvents
{
    public static event Action LevelChanging;
    
    public static void RestartLevel()
        => ChangeLevel(SceneManager.GetActiveScene().name);

    public static void NavigateOnMainMenu()
        => ChangeLevel(GameConstants.MainMenuSceneName);

    public static AsyncOperation NavigateOnMainMenuAsync(bool allowSceneActivation = true)
        => ChangelevelAsync(GameConstants.MainMenuSceneName, allowSceneActivation);

    public static void NavigateOnGame()
        => ChangeLevel(GameConstants.GameSceneName);
    
    private static void ChangeLevel(string sceneName)
    {
        InvokeLevelChanging();
        SceneManager.LoadScene(sceneName);
    }

    private static AsyncOperation ChangelevelAsync(string sceneName, bool allowSceneActivation = true)
    {
        InvokeLevelChanging();
        var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = allowSceneActivation;
        return asyncOperation;
    }

    private static void InvokeLevelChanging()
        => LevelChanging?.Invoke();
}