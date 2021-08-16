using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayService : MonoBehaviour
{
    private bool isSaving = false;

    private void Start() {
        Init();
    }

    private void Init() {
        PlayGamesClientConfiguration conf = new PlayGamesClientConfiguration.Builder()
            .RequestServerAuthCode(false)
            .EnableSavedGames()
            .Build();

        PlayGamesPlatform.InitializeInstance(conf);
        PlayGamesPlatform.Activate();
        Debug.Log("PlayGames Services Initial");
        SingIn();
    }

    private void SingIn() {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (succes) => {
            if (succes == SignInStatus.Success)
                Debug.Log("Success signin");
            else
                Debug.Log("Error signin");
        });
    }

    
    
}
