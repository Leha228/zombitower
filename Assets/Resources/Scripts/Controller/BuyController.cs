using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyController : MonoBehaviour
{
    public void Buy(string name) {
        string value = gameObject.GetComponentInChildren<Text>().text;

        if (value == UserModel.PAYMENT)
            return;
        else
            Qullet(Convert.ToInt32(value), name);
    }

    private void Qullet(int price, string name) {
        if (price > UserModel.singleton.GetGold()) return;
        UserModel.singleton.gold -= price;

        if (name == UserModel.TOWER)
            UserModel.singleton.towers.Add(Convert.ToInt32(gameObject.name));
        else if (name == UserModel.HEROUS)
            UserModel.singleton.herous.Add(Convert.ToInt32(gameObject.name));

        gameObject.GetComponent<Image>().color = ShopController.singleton.SetColor(Color.green, 0.4f);
        gameObject.GetComponentInChildren<Text>().text = UserModel.PAYMENT;
        UserController.singleton.UpdateCashList();
        SaveData.singleton.SaveToFile();
    }
}
