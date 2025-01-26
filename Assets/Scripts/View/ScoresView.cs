using TMPro;
using UnityEngine;
using Zenject;

public sealed class ScoresView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    [Inject]
    public void Init(ScoresOnLevel scores)
    {
        _scoresOnLevel = scores;

        _scoresOnLevel.ScoreChanged += UpdateText;
        UpdateText(scores.Score);

        GameEvents.LevelChanging += OnLevelChanging;
    }

    private void OnLevelChanging()
    {
        _scoresOnLevel.ScoreChanged -= UpdateText;
        GameEvents.LevelChanging -= OnLevelChanging;
    }

    private void UpdateText(int score)
    {
        _text.text = NumberToSpritesConverter.Convert(score);
    }

    private ScoresOnLevel _scoresOnLevel;
}