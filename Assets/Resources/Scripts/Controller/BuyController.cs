using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyController : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons;

    public void Buy() {
        string value = gameObject.GetComponentInChildren<Text>().text;

        if (value == UserModel.ACTIVE) 
            return;
        else if (value == UserModel.SELECT) 
            SelectTab();
        else
            Qullet(Convert.ToInt32(value));
    }

    private void SelectTab() {
        int currentTower = PlayerPrefs.GetInt(UserModel.ACTIVE_TOWER);
        buttons[currentTower].GetComponentInChildren<Text>().text = UserModel.SELECT;
        PlayerPrefs.SetInt(UserModel.ACTIVE_TOWER, Convert.ToInt32(gameObject.name));
        gameObject.GetComponentInChildren<Text>().text = UserModel.ACTIVE;
    }

    private void Qullet(int price) {
        if (price > UserModel.singleton.GetGold()) return;
        PlayerPrefs.SetInt(UserModel.GOLD, PlayerPrefs.GetInt(UserModel.GOLD) - price);
        gameObject.GetComponent<Image>().color = ShopController.singleton.SetColor(Color.green, 0.4f);
        gameObject.GetComponentInChildren<Text>().text = UserModel.SELECT;
        UserController.singleton.UpdateCashList();
    }
}
