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
}
