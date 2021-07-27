using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] listLevel;

    public void DownloadLevel(int level) {
        DataHolder.numberLevel = level;
        DataHolder.listEnemy = listLevel[level - 1].GetComponent<LevelModel>().listEnemy;
        SceneManager.LoadScene("Game");
    }
}
