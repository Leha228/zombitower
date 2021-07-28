using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levelList;
    [SerializeField] private Sprite[] spriteList;

    private void Start() {
        //PlayerPrefs.DeleteAll();
        int countLevel = PlayerPrefs.GetInt("countLevel", 1);

        for (int currentLevel = 0; currentLevel < countLevel; currentLevel++) {
            if (currentLevel < countLevel) {
                levelList[currentLevel].GetComponent<Button>().interactable = true;
                if (currentLevel == countLevel - 1) 
                    levelList[currentLevel].GetComponent<Image>().sprite = spriteList[1];
                else
                    levelList[currentLevel].GetComponent<Image>().sprite = spriteList[0];
            } else {
                levelList[currentLevel].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void DownloadLevel(int level) {
        DataHolder.numberLevel = level;
        DataHolder.enemyList = levelList[level - 1].GetComponent<LevelModel>().enemyList;
        SceneManager.LoadScene("Game");
    }
}
