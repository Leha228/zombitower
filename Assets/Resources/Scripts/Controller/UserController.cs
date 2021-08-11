using UnityEngine;

public class UserController : MonoBehaviour
{
    public static UserController singleton { get; private set;}
    private void Awake() { singleton = this; }

    private void Start() {
        //PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey(UserModel.ACTIVE_TOWER))
            Init();

        SaveData.singleton.LoadToFile();
        UpdateCashList();
    }

    public void UpdateCashList() {
        foreach (var item in UserModel.singleton.cashList)
            item.text = item.name == "gold" ? 
                UserModel.singleton.GetGold().ToString() : UserModel.singleton.GetDiamond().ToString();
    }

    private void Init() {
        PlayerPrefs.SetInt(UserModel.ACTIVE_TOWER, 0);
        PlayerPrefs.SetInt(UserModel.RESOURCE_WOOD, 5);
        PlayerPrefs.SetInt(UserModel.RESOURCE_IRON, 5);
        PlayerPrefs.SetInt(UserModel.RESOURCE_CHARTER, 5);
        PlayerPrefs.SetInt(UserModel.ARROWS, 50);
        UserModel.singleton.towers.Add(0);
        SaveData.singleton.SaveToFile();
    }
}
