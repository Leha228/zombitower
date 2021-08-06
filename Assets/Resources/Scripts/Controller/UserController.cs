using UnityEngine;

public class UserController : MonoBehaviour
{
    private void Start() {
        PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey(UserModel.DIAMOND))
            Init();

        foreach (var item in UserModel.singleton.cashList)
            item.text = PlayerPrefs.GetInt(item.name, 0).ToString();
    }

    private void Init() {
        PlayerPrefs.SetInt(UserModel.DIAMOND, UserModel.singleton.diamond);
        PlayerPrefs.SetInt(UserModel.GOLD, UserModel.singleton.gold);
        PlayerPrefs.SetInt(UserModel.ACTIVE_TOWER, 0);
        PlayerPrefs.SetInt(UserModel.RESOURCE_WOOD, 0);
        PlayerPrefs.SetInt(UserModel.RESOURCE_IRON, 0);
        PlayerPrefs.SetInt(UserModel.RESOURCE_CHARTER, 0);
    }
}
