using TMPro;
using UnityEngine;
using Zenject;

public sealed class ScoresView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    private void Awake()
    {
        _text.text = 0.ToString();
    }

    [Inject]
    public void Init(ScoresOnLevel scores)
    {
        _scoresOnLevel = scores;

        _scoresOnLevel.ScoreChanged += UpdateText;
        UpdateText(scores.Score);

        GameEvents.GameEnded += OnGameEnd;
        GameEvents.LevelChanging += OnLevelChanging;
    }

    private void OnLevelChanging()
    {
        _scoresOnLevel.ScoreChanged -= UpdateText;
        GameEvents.GameEnded -= OnGameEnd;
        GameEvents.LevelChanging -= OnLevelChanging;
    }

    private void OnGameEnd()
    {
        gameObject.SetActive(false);
    }

    private void UpdateText(int score)
    {
        _text.text = score.ToString();
    }

    private ScoresOnLevel _scoresOnLevel;
}