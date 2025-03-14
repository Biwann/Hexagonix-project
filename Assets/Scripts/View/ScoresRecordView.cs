using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoresRecordView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    [Inject]
    public void Init(IScoresRecord scoreRecord)
    {
        _scoresRecord = scoreRecord;
        UpdateText(_scoresRecord.ScoreRecord);

        _scoresRecord.ScoresRecordChanged += UpdateText;
        GlobalProgramEvents.LevelChanging += OnLevelChanging;
    }

    private void OnLevelChanging()
    {
        _scoresRecord.ScoresRecordChanged -= UpdateText;
        GlobalProgramEvents.LevelChanging -= OnLevelChanging;
    }

    private void UpdateText(int score)
    {
        _text.text = score.ToString();
    }

    private IScoresRecord _scoresRecord;
}
