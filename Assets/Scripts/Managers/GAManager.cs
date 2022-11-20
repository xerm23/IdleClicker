using UnityEngine;
using GameAnalyticsSDK;
using IdleClicker.Managers;
using System;

public class GAManager : MonoBehaviour
{
    private void Start()
    {
        GameAnalytics.Initialize();
        SendEvent("Game Loaded", 1);
        ScoreManager.OnTotalMergesChanged += SendEventForMerges;
        GameManager.OnGamePaused += SendEventForPauses;
        GameManager.OnPlayerStartedGame += SendEventForPlayerStart;
    }
    private void OnDestroy()
    {
        ScoreManager.OnTotalMergesChanged -= SendEventForMerges;
        GameManager.OnGamePaused -= SendEventForPauses;
        GameManager.OnPlayerStartedGame -= SendEventForPlayerStart;
    }

    private void SendEventForPlayerStart() => SendEvent("PlayerStartedTheGame", 1);
    private void SendEventForMerges(int totalMerges) => SendEvent("Merges", totalMerges);
    private void SendEventForPauses(int totalPauses) => SendEvent("Pauses", totalPauses);

    private void SendEvent(string name, float eventValue) => GameAnalytics.NewDesignEvent(name, eventValue);

}
