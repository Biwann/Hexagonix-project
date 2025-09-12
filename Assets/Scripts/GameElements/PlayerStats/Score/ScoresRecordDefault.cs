using System;
using UnityEngine;

public sealed class ScoresRecordDefault : IScoresRecord
{
    public event Action<int> ScoresRecordChanged;

    public ScoresRecordDefault(IDataSaver dataSaver)
    {
        _dataSaver = dataSaver;
        ScoreRecord = _dataSaver.GetInt(GameConstants.SavingScoreRecordName, defaultValue: 0);
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
        _dataSaver.SetInt(GameConstants.SavingScoreRecordName, ScoreRecord);
        
        return true;
    }

    private readonly IDataSaver _dataSaver;
    private int _scoreRecord = 0;
}