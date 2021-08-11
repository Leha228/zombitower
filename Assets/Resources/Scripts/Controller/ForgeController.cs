using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class ForgeController : MonoBehaviour
{

    private void Start() {
        foreach (var item in UserModel.singleton.resourceList)
            if (PlayerPrefs.GetInt(item.name, 0) > 0) 
                item.color = SetColor(item, 1f);
            else 
                item.color = SetColor(item, 0.5f);    
    }

    private Color SetColor(Image item, float value) {
        Image image = item.GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = value;
        return tempColor;
    }

    public void SelectTab(string name) {
        foreach (var item in ForgeModel.singleton.resourceList) {    
            if (item.name == name)
                item.SetActive(true);
            else   
                item.SetActive(false);
        }
    }

    public void PayArrow() {
        if (UserModel.singleton.gold < 5 
            || PlayerPrefs.GetInt(UserModel.RESOURCE_IRON, 0) < 1 
            || PlayerPrefs.GetInt(UserModel.RESOURCE_WOOD, 0) < 1) return;

        UserModel.singleton.gold -= 5;
        PlayerPrefs.SetInt(UserModel.RESOURCE_IRON, UserModel.singleton.GetResourceIron() - 1);
        PlayerPrefs.SetInt(UserModel.RESOURCE_WOOD, UserModel.singleton.GetResourceWood() - 1);
        PlayerPrefs.SetInt(UserModel.ARROWS, UserModel.singleton.GetArrows() + 1);
        SaveData.singleton.SaveToFile();
        UserController.singleton.UpdateCashList();
        MenuManager.singleton.UpdateCountList();
    }
}
