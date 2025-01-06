using System;
using Unity.VisualScripting.Dependencies.NCalc;

public sealed class ExpirienceLevelProvider
{
    public const int OneLevelMaxPoints = 500;

    public ExpirienceLevelProvider(
        ExpirienceLocal expirience,
        IExpirienceSaver expirienceSaver,
        Tracer tracer)
    {
        _expirience = expirience;
        _currentLevel = MakeLevelInformation(expirienceSaver.SavedExpirience);
        _tracer = tracer;

        _expirience.ExpirienceChanged += OnExpirienceChanged;
    }

    public event Action<LevelInformation> LevelInformationChanged;
    public event Action LevelUp;

    public LevelInformation CurrentLevelInformation
    {
        get
        {
            return _currentLevel;
        }
        private set
        {
            if (_currentLevel.MoreThan(value))
            {
                _tracer.TraceDebug($"cant set new level information {value}");
                return;
            }

            var prevLevel = _currentLevel.Level;
            _tracer.TraceDebug($"set new level information - {value}");
            _currentLevel = value;
            LevelInformationChanged?.Invoke(_currentLevel);

            if (_currentLevel.Level > prevLevel)
            {
                _tracer.TraceDebug($"Level up - {value}");
                LevelUp?.Invoke();
            }
        }
    }

    private void OnExpirienceChanged(int newExpirience)
    {
        CurrentLevelInformation = MakeLevelInformation(newExpirience);
    }

    private LevelInformation MakeLevelInformation(int expirience)
    {
        var level = 0;
        var expirienceLeft = expirience;

        for (; (level + 1) * OneLevelMaxPoints <= expirienceLeft; level++)
        {
            expirienceLeft -= (level + 1) * OneLevelMaxPoints;
        }

        var levelPoints = expirienceLeft;
        var maxLevelPoints = (level + 1) * OneLevelMaxPoints;

        return new(level, levelPoints, maxLevelPoints);
    }

    private LevelInformation _currentLevel;
    private readonly Tracer _tracer;
    private readonly ExpirienceLocal _expirience;
}
