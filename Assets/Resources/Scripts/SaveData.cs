using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.IO;    

public class SaveData : MonoBehaviour
{
    public static SaveData singleton {get; private set;}
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

        string json = JsonUtility.ToJson(data);

        if (!File.Exists(_pathSave))
            File.Create(_pathSave).Close();

        File.WriteAllText(_pathSave, json);
        Debug.Log("Save");
    }

    public void LoadToFile() {
        if (!File.Exists(_pathSave)) {
            Debug.Log("File not found");
            return;
        }
        try {
            string json = File.ReadAllText(_pathSave);
            SaveModel saveModelFromJson = JsonUtility.FromJson<SaveModel>(json);
            UserModel.singleton.diamond = saveModelFromJson.diamond;
            UserModel.singleton.gold = saveModelFromJson.gold;
            //UserModel.singleton.towers = saveModelFromJson.towers;
        } catch {
            Debug.Log("Not load to file");
        }
    }
    
    [Serializable]
    public class Data
    {
        public int gold;
        public int diamond;
    }
}
