using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuWindowView : MonoBehaviour
{
    public void OnStartGameClicked()
        => GlobalProgramEvents.NavigateOnGame();
}
