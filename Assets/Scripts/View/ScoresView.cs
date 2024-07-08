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
        scores.ScoreChanged += UpdateText;
        UpdateText(scores.Score);
    }

    private void UpdateText(int score)
    {
        _text.text = score.ToString();
    }
}