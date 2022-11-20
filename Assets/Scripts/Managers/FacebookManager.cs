using Facebook.Unity;
using IdleClicker.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour
{
    [SerializeField] private Button _logInWithFacebookBtn;

    private void Awake()
    {
        _logInWithFacebookBtn.onClick.AddListener(FacebookLogin);

        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            FB.Android.RetrieveLoginStatus(LoginStatusCallback);
        }
        else
        {
            //Handle FB.Init
            FB.Init(() =>
            {
                FB.ActivateApp();
                FB.Android.RetrieveLoginStatus(LoginStatusCallback);
            });
        }
        ScoreManager.OnTotalMergesChanged += SendEventForMerges;
        GameManager.OnGamePaused += SendEventForPauses;
        GameManager.OnPlayerStartedGame += SendEventForPlayerStart;
    }
    private void OnDestroy()
    {
        ScoreManager.OnTotalMergesChanged -= SendEventForMerges;
        GameManager.OnGamePaused -= SendEventForPauses;
        GameManager.OnPlayerStartedGame -= SendEventForPlayerStart;
        _logInWithFacebookBtn.onClick.RemoveListener(FacebookLogin);
    }

    // Unity will call OnApplicationPause(false) when an app is resumed
    // from the background
    void OnApplicationPause(bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        if (!pauseStatus)
        {
            //app resume
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                //Handle FB.Init
                FB.Init(() =>
                {
                    FB.ActivateApp();
                });
            }
        }
    }

    public void FacebookLogin()
    {
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    private void LoginStatusCallback(ILoginStatusResult result)
    {
        if (!string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Error: " + result.Error);
        }
        else if (result.Failed)
        {
            Debug.Log("Failure: Access Token could not be retrieved");
        }
        else
        {
            SendEvent("Game Loaded", 1);
            Debug.Log("Success: " + result.AccessToken.UserId);
        }
    }
    private void SendEvent(string eventName, float value)
    {
        Debug.Log($"Sending FB event: {eventName} with value {value}");
        FB.LogAppEvent(eventName, value);
    }
    private void SendEventForPlayerStart() => SendEvent("PlayerStartedTheGame", 1);
    private void SendEventForMerges(int totalMerges) => SendEvent("Merges", totalMerges);
    private void SendEventForPauses(int totalPauses) => SendEvent("Pauses", totalPauses);







}
