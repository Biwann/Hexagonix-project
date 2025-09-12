public static class PauseController
{
    public static bool IsPaused { get; private set; } = false;

    public static void SetPause(bool isPause)
        => IsPaused = isPause;
}