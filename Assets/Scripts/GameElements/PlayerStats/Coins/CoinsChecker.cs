public sealed class CoinsChecker
{
    public CoinsChecker(
        ICoinsSaver coinsSaver,
        CoinsLocal coinsLocal)
    {
        _coinsSaver = coinsSaver;
        _coinsLocal = coinsLocal;

        var diff = _coinsSaver.SavedCoins - _coinsLocal.Coins;
        _coinsLocal.AddCoins(diff);

        _coinsLocal.CoinsChanged += OnLocalCoinsChanged;
    }

    private void OnLocalCoinsChanged(int coins)
    {
        _coinsSaver.TryChangeSavedCoins(coins);
    }

    private readonly ICoinsSaver _coinsSaver;
    private readonly CoinsLocal _coinsLocal;
}