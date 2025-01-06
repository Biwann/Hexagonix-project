using System;
using UnityEngine;

public sealed class ScoresRecordDefault : IScoresRecord
{
    public event Action<int> ScoresRecordChanged;

    public ScoresRecordDefault()
    {
        ScoreRecord = PlayerPrefs.GetInt(GameConstants.SavingScoreRecordName, defaultValue: 0);
    }

    public int ScoreRecord
    {
        get
        {
            return _scoreRecord;
        }
        private set
        {
            if (value == _scoreRecord)
            {
                return;
            }

            _scoreRecord = value;
            ScoresRecordChanged?.Invoke(_scoreRecord);
        }

    }

    public bool TryChangeScoreRecord(int score)
    {
        if (score <= ScoreRecord) 
        {
            return false;
        }

        ScoreRecord = score;
        PlayerPrefs.SetInt(GameConstants.SavingScoreRecordName, ScoreRecord);
        PlayerPrefs.Save();
        
        return true;
    }

    private int _scoreRecord = 0;
}