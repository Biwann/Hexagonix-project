public sealed class AddExpirienceOnScoreTracker
{
    public AddExpirienceOnScoreTracker(
        ExpirienceLocal expirience,
        ScoresOnLevel scoresOnLevel)
    {
        _lastTrackedScore = 0;
        _expirience = expirience;

        OnScoreChanged(scoresOnLevel.Score);
        scoresOnLevel.ScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int updatedScore)
    {
        if (updatedScore > _lastTrackedScore)
        {
            var diff = updatedScore - _lastTrackedScore;
            _expirience.AddExpirience(diff);
            _lastTrackedScore = updatedScore;
        }
    }

    private int _lastTrackedScore;
    private readonly ExpirienceLocal _expirience;
}