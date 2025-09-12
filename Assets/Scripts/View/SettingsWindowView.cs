using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public sealed class SettingsWindowView : MonoBehaviour
{
    [SerializeField] private GameObject _goMainMenuButton;

    private void OnEnable()
    {
        Debug.Log($"Enable settings window in {SceneManager.GetActiveScene().name}");
        _goMainMenuButton.SetActive(CanGoMainMenu());
        PauseController.SetPause(true);

        GlobalProgramEvents.LevelChanging += CloseWindow;
    }

    private void OnDisable()
    {
        PauseController.SetPause(false);
        GlobalProgramEvents.LevelChanging -= CloseWindow;
    }

    public void GoMainMenu()
    {
        GameEvents.NavigateOnMainMenu();
    }

    private void CloseWindow() => gameObject?.SetActive(false);

    private bool CanGoMainMenu() => SceneManager.GetActiveScene().name != GameConstants.MainMenuSceneName;
}
