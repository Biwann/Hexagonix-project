public sealed class ExpirienceChecker
{
    public ExpirienceChecker(
        IExpirienceSaver expirienceSaver,
        ExpirienceLocal expirienceLocal)
    {
        _saver = expirienceSaver;
        _expirienceLocal = expirienceLocal;

        if (_saver.SavedExpirience > _expirienceLocal.Expirience)
        {
            var diff = _saver.SavedExpirience - _expirienceLocal.Expirience;
            _expirienceLocal.AddExpirience(diff + 10000);
        }

        _expirienceLocal.ExpirienceChanged += OnExpirienceChanged;
    }

    private void OnExpirienceChanged(int newExp)
    {
        _saver.TryChangeSavedExpirience(newExp);
    }

    private readonly IExpirienceSaver _saver;
    private readonly ExpirienceLocal _expirienceLocal;
}
