using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class LooseWindowView : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;

    [Inject]
    public void Inject(ScoresOnLevel scoresOnLevel)
    {
        _scoresOnLevel = scoresOnLevel;

        GameEvents.GameEnded += OnGameEnd;
        GameEvents.LevelChanging += OnRestarting;

        gameObject.SetActive(false);
    }

    public void OnRestartButtonClick()
        => GameEvents.RestartLevel();

    public void OnMainMenuButtonClick()
        => GameEvents.NavigateOnMainMenu();

    private void OnGameEnd()
    {
        gameObject.SetActive(true);
        _scoreText.text = _scoresOnLevel.Score.ToString();
    }

    private void OnRestarting()
    {
        GameEvents.GameEnded -= OnGameEnd;
        GameEvents.LevelChanging -= OnRestarting;
    }

    private ScoresOnLevel _scoresOnLevel;
}
