public struct LevelInformation
{
    public LevelInformation(
        int level,
        int levelPoints,
        int maxLevelPoints)
    {
        Level = level;
        LevelPoints = levelPoints;
        MaxLevelPoints = maxLevelPoints;
    }

    public int Level { get; }
    public int LevelPoints { get; }
    public int MaxLevelPoints { get; }

    public bool MoreThan(LevelInformation level)
    {
        return Level > level.Level || (Level == level.Level && LevelPoints > level.LevelPoints);
    }

    public override string ToString()
    {
        return $"Level: {Level}, {LevelPoints}/{MaxLevelPoints}";
    }
}