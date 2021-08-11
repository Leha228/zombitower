using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyController : MonoBehaviour
{
    public void Buy() {
        string value = gameObject.GetComponentInChildren<Text>().text;

        if (value == UserModel.PAYMENT) 
            return;
        else
            Qullet(Convert.ToInt32(value));
    }

    private void Qullet(int price) {
        if (price > UserModel.singleton.GetGold()) return;
        UserModel.singleton.gold -= price;
        UserModel.singleton.towers.Add(Convert.ToInt32(gameObject.name));
        gameObject.GetComponent<Image>().color = ShopController.singleton.SetColor(Color.green, 0.4f);
        gameObject.GetComponentInChildren<Text>().text = UserModel.PAYMENT;
        UserController.singleton.UpdateCashList();
        SaveData.singleton.SaveToFile();
    }
}
