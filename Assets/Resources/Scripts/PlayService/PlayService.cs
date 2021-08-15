using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayService : MonoBehaviour
{

    private void Start() {
        Init();
    }

    private void Init() {
        PlayGamesClientConfiguration conf = new PlayGamesClientConfiguration.Builder()
            .RequestServerAuthCode(false)
            .Build();

        PlayGamesPlatform.InitializeInstance(conf);
        PlayGamesPlatform.Activate();
        Debug.Log("PlayGames Services Initial");
        SingIn();
    }

    private void SingIn() {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (succes) => {
            if (succes == SignInStatus.Success)
                Debug.Log("SignIn Success");
            else
                Debug.Log("Error signin");
        });
    }
}
