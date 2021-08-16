using System;
using System.Text;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class PlayServiceSave : MonoBehaviour
{
    public static PlayServiceSave singleton { get; private set; }
    private bool isSaving;
    private System.DateTime startDateTime;

    private void Awake() { singleton = this; }

    private void Start() {
        startDateTime = System.DateTime.Now;
    }

    public void OpenSavedGame(bool saving) {
        isSaving = saving;
        OpenSavedGame("SaveGame");
    }

    private void OpenSavedGame(string filename) {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game) {
        if (status == SavedGameRequestStatus.Success) {
            if (isSaving)  
                SaveGame(game, SaveData.singleton.GetDataSave());
            else 
                LoadGameData(game);
        } else {
            
        }
    }

    private void SaveGame (ISavedGameMetadata game, byte[] savedData) {
        TimeSpan currentSpan = System.DateTime.Now - startDateTime;
        TimeSpan totalPlayTime = game.TotalTimePlayed + currentSpan;
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlayTime)
            .WithUpdatedDescription("Saved game at " + System.DateTime.Now);
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    private void OnSavedGameWritten (SavedGameRequestStatus status, ISavedGameMetadata game) {
        if (status == SavedGameRequestStatus.Success) {
            // handle reading or writing of saved game.
        } else {
            // handle error
        }
    }

    private void LoadGameData (ISavedGameMetadata game) {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    private void OnSavedGameDataRead (SavedGameRequestStatus status, byte[] data) {
        if (status == SavedGameRequestStatus.Success) {
            if (data.Length > 0) {
                string dataGoogle = Encoding.ASCII.GetString(data);
                SaveData.singleton.GetDataLoad(dataGoogle);
            } else {
                UserController.singleton.Init();
            }
        } else {
            // handle error
        }
    }
}
