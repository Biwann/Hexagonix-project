public sealed class RecordChecker
{
    public RecordChecker(
        ScoresOnLevel scoresOnLevel,
        IScoresRecord scoresRecord) 
    {
        _scoresOnLevel = scoresOnLevel;
        _scoresRecord = scoresRecord;

        GameEvents.GameEnded += OnGameEnded;
    }

    private void OnGameEnded()
        => _scoresRecord.TryChangeScoreRecord(_scoresOnLevel.Score);

    private readonly ScoresOnLevel _scoresOnLevel;
    private readonly IScoresRecord _scoresRecord;
}