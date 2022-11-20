using System;
public static class GameManager 
{
    private static int _totalPausesByPlayer = 0;
    private static int _totalResumesByPlayer = 0;

    public static Action<int> OnGamePaused;
    public static Action<int> OnGameResumed;
    public static Action OnPlayerStartedGame;

    public static void PauseGame()
    {
        OnGamePaused?.Invoke(++_totalPausesByPlayer);
    }

    public static void ResumeGame()
    {
        OnGameResumed?.Invoke(++_totalResumesByPlayer);
    }

    public static void PlayerStartedGame()
    {
        OnPlayerStartedGame?.Invoke();
    }

}
