using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager singleton { get; private set; }
    [SerializeField] private GameObject[] levelList;
    [SerializeField] private Sprite[] spriteList;
    [SerializeField] private GameObject[] menuList;
    [SerializeField] private Text[] countList;

    private void Awake() { singleton = this; }

    private void Start() {
        //PlayerPrefs.DeleteAll();
        int countLevel = PlayerPrefs.GetInt("countLevel", 1);

        if (countLevel > levelList.Length)
            countLevel = levelList.Length;

        if (DataHolder.isNext)
            DownloadLevel(countLevel);

        for (int currentLevel = 0; currentLevel < countLevel; currentLevel++) {
            if (currentLevel < countLevel) {
                levelList[currentLevel].GetComponent<Button>().interactable = true;
                if (currentLevel == countLevel - 1)
                    CurrentLevel(currentLevel);
                else
                    levelList[currentLevel].GetComponent<Image>().sprite = spriteList[0];
            } else {
                levelList[currentLevel].GetComponent<Button>().interactable = false;
            }
        }

        UpdateCountList();
    }

    public void DownloadLevel(int level) {
        DataHolder.numberLevel = level;
        DataHolder.gold = levelList[level - 1].GetComponent<LevelModel>().gold;
        DataHolder.enemyList = levelList[level - 1].GetComponent<LevelModel>().enemyList;
        DataHolder.isNext = false;
        SceneManager.LoadScene("Game");
    }

    public void Open(string name) {
        foreach (var item in menuList) {
            if (item.name == name)
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    public void OpenMenu() => SceneManager.LoadScene("Menu");

    public void UpdateCountList() {
        foreach (var item in countList)
            item.text = UserModel.singleton.GetArrows().ToString();
    }

    private void CurrentLevel(int currentLevel) {
        levelList[currentLevel].GetComponent<Image>().sprite = spriteList[1];
        levelList[currentLevel].GetComponent<RectTransform>().sizeDelta = new Vector2(
            160f,
            50f
        );
    }
}
