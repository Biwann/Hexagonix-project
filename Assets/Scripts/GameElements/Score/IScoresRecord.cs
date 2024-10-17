using System;

public interface IScoresRecord
{
    event Action<int> ScoresRecordChanged;

    int ScoreRecord { get; }

    bool TryChangeScoreRecord(int score);
}