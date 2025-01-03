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
        _scoreText.text = NumberToSpritesConverter.Convert(_scoresOnLevel.Score);
    }

    private void OnRestarting()
    {
        GameEvents.GameEnded -= OnGameEnd;
        GameEvents.LevelChanging -= OnRestarting;
    }

    private ScoresOnLevel _scoresOnLevel;
}
