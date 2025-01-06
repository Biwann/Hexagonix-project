using System;

public sealed class ScoresOnLevel
{
    public ScoresOnLevel()
    {
        Score = 0;    
    }

    public event Action<int> ScoreChanged;

    public int Score
    {
        get
        {
            return _score;
        }
        private set
        {
            if (_score == value)
            {
                return;
            }

            _score = value;
            ScoreChanged?.Invoke(_score);
        }
    }

    public void AddScore(int add)
    {
        Score += add;
    }

    private int _score;
}
