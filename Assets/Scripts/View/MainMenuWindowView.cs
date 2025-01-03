using UnityEngine;

public class MainMenuWindowView : MonoBehaviour
{
    public void OnStartGameClicked()
        => GlobalProgramEvents.NavigateOnGame();
}
