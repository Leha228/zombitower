using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public static ShopController singleton { get; private set;}
    private void Awake() { singleton = this; }

    private void Start() {
        UpdatePriceList(ShopModel.singleton.herous);
        UpdatePriceList(ShopModel.singleton.towers);
    }

    private void UpdatePriceList(Button[] array) {
        foreach (var item in array) {
            int name = Convert.ToInt32(item.name);
            int price = GetPrice(item.GetComponentInChildren<Text>().text);
            var image = item.GetComponent<Image>();
            item.GetComponent<Image>().color = SetColor(Color.green, 0.4f);

            if (UserModel.singleton.towers.Contains(name)) {
                item.GetComponentInChildren<Text>().text = UserModel.PAYMENT;
                continue;
            }

            if (price > UserModel.singleton.GetGold())
                item.GetComponent<Image>().color =  SetColor(Color.red, 0.4f);
        }
    }

    private int GetPrice(string value) {
        try { return Convert.ToInt32(value); }
        catch (System.Exception) { return 0; }
    }

    public Color SetColor(Color color, float value) {
        var tempColor = color;
        tempColor.a = value;
        return tempColor;
    }

    public void SelectTab(string name) {
        foreach (var tab in ShopModel.singleton.tabs) {
            if (tab.name == name)
                tab.SetActive(true);
            else
                tab.SetActive(false);
        }
    }

    public void BackToMenu() => MenuManager.singleton.Open("Map");
}
