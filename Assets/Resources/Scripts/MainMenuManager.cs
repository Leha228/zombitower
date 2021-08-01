using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] settingOpenClose;

    public void Play() => SceneManager.LoadScene("Map");

    public void SettingOpenClose() {
        foreach (var item in settingOpenClose)
            item.SetActive(!item.activeSelf); 
    }
}
