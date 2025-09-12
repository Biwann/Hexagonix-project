using TMPro;
using UnityEngine;
using Zenject;

public class LooseWindowView : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] GameObject _youScore;
    [SerializeField] GameObject _newRecord;

    [Inject]
    public void Inject(
        ScoresOnLevel scoresOnLevel,
        IScoresRecord scoresRecord)
    {
        _scoresOnLevel = scoresOnLevel;
        _scoresRecord = scoresRecord;

        GameEvents.GameEnded += OnGameEnd;
        GameEvents.LevelChanging += OnRestarting;

        gameObject.SetActive(false);
    }

    public bool IsRecord => _scoresOnLevel.Score == _scoresRecord.ScoreRecord;

    public void OnRestartButtonClick()
        => GameEvents.RestartLevel();

    public void OnMainMenuButtonClick()
        => GameEvents.NavigateOnMainMenu();

    private void OnGameEnd()
    {
        var isRecord = IsRecord;
        gameObject.SetActive(true);

        _youScore.SetActive(!isRecord);
        _newRecord.SetActive(isRecord);

        if (isRecord)
            GameAudioController.Instance.PlayNewRecord();

        _scoreText.text = _scoresOnLevel.Score.ToString();
    }

    private void OnRestarting()
    {
        GameEvents.GameEnded -= OnGameEnd;
        GameEvents.LevelChanging -= OnRestarting;
    }

    private ScoresOnLevel _scoresOnLevel;
    private IScoresRecord _scoresRecord;
}
