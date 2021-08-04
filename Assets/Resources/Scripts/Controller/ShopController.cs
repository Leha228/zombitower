using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
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
