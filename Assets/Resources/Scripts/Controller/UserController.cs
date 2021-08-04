using UnityEngine;

public class UserController : MonoBehaviour
{
    private void Start() {
        PlayerPrefs.SetInt("diamond", UserModel.singleton.diamond);
        PlayerPrefs.SetInt("gold", UserModel.singleton.gold);

        foreach (var item in UserModel.singleton.cashList)
            item.text = PlayerPrefs.GetInt(item.name, 0).ToString();
    }
}
