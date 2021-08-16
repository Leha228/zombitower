using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System.IO;    

public class SaveData : MonoBehaviour
{
    public static SaveData singleton { get; private set; }
    private string _pathSave;
    private string _saveFileName = "data.json";

    private void Awake() 
    {
#if UNITY_ANDROID && UNITY_EDITOR
        _pathSave = Path.Combine(Application.persistentDataPath, _saveFileName);
#else
        _pathSave = Path.Combine(Application.dataPath, _saveFileName);
#endif
        singleton = this;
    }

    public void SaveToFile() {
        Data data = new Data();
        data.gold = UserModel.singleton.GetGold();
        data.diamond = UserModel.singleton.GetDiamond();
        data.towers = UserModel.singleton.towers;
        data.levels = PlayerPrefs.GetInt("countLevel", 1);

        string json = JsonUtility.ToJson(data);

        if (!File.Exists(_pathSave))
            File.Create(_pathSave).Close();

        File.WriteAllText(_pathSave, json);
        Debug.Log("Save");
    }

    public byte[] GetDataSave() {
        Data data = new Data();
        data.gold = UserModel.singleton.GetGold();
        data.diamond = UserModel.singleton.GetDiamond();
        data.towers = UserModel.singleton.towers;
        data.levels = PlayerPrefs.GetInt("countLevel", 1);

        string json = JsonUtility.ToJson(data);

        byte[] barray = Encoding.UTF8.GetBytes(json);
        return barray;
    }

    public void GetDataLoad(string json) {
        Data saveModelFromJson = JsonUtility.FromJson<Data>(json);
        UserModel.singleton.diamond = saveModelFromJson.diamond;
        UserModel.singleton.gold = saveModelFromJson.gold;
        UserModel.singleton.towers = saveModelFromJson.towers;
        PlayerPrefs.SetInt("countLevel", saveModelFromJson.levels);
    }

    public void LoadToFile() {
        if (!File.Exists(_pathSave)) return;

        string json = File.ReadAllText(_pathSave);
        Data saveModelFromJson = JsonUtility.FromJson<Data>(json);
        UserModel.singleton.diamond = saveModelFromJson.diamond;
        UserModel.singleton.gold = saveModelFromJson.gold;
        UserModel.singleton.towers = saveModelFromJson.towers;
        PlayerPrefs.SetInt("countLevel", saveModelFromJson.levels);
        Debug.Log("Data to load...");
    }

    private void OnApplicationQuit() { 
        SaveToFile();
        try { PlayServiceSave.singleton.OpenSavedGame(true); }
        catch { Debug.Log("Not save cloud");}
    }
    
    [Serializable]
    public class Data
    {
        public int gold;
        public int diamond;
        public List<int> towers;
        public int levels;
    }
}
