using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static EventManager singleton { get; private set; }

    [SerializeField] public GameObject menu;
    [SerializeField] public GameObject youDie;
    [SerializeField] public GameObject youDieContinue;
    [SerializeField] public GameObject youWin;
    [SerializeField] private GameObject herous;
    [SerializeField] private GameObject[] noActiveObject;
    [SerializeField] private Text[] youWinRourcesText;
    private int _progress = 0;
    private int _numberLevel = DataHolder.numberLevel;
    private int _gold = DataHolder.gold;
    private int _countEnemy = DataHolder.enemyList.Length;

    private void Awake() { singleton = this; }

    private void OpenGate() => TowerModel.singleton.SetAnimation(TowerModel.singleton.open[UserModel.singleton.GetActiveTower()], false);
    private void CloseGate() => TowerModel.singleton.SetAnimation(TowerModel.singleton.close[UserModel.singleton.GetActiveTower()], false);
    public int GetProgress() => _progress;
    public void YouDieContinueOpen() => youDieContinue.SetActive(true);
    public void YouDieContinueClose() => youDieContinue.SetActive(false);
    public void RecoveryHerous() => herous.SetActive(true);
    public void YouDieExit() => SceneManager.LoadScene("Map");
    public void YouDieRestart() => SceneManager.LoadScene("Game");
    public void YouWinExit() => SceneManager.LoadScene("Map");

    public void CreateMob() {
        OpenGate();

        GameObject enemyCopy = Instantiate(TowerModel.singleton.mobs[UserModel.singleton.GetActiveTower()]);
        enemyCopy.transform.position = MobModel.singleton.transform.position;

        Invoke("CloseGate", 2f);
    }

    public void SetProgress() {
        _progress = (int)(100 / _countEnemy) + _progress;
        if (_progress >= 99) Invoke("nextLevel", 3f);
    }

    public void OnMenu() {
        menu.SetActive(true);
        Time.timeScale = 0f;
        NoActiveObject();
    }

    public void OnResume() {
        menu.SetActive(false);
        Time.timeScale = 1f;
        NoActiveObject();
    }

    public void YouDie() {
        youDie.SetActive(true);
        Time.timeScale = 0f;
        NoActiveObject();
    }

    public void YouWin() {
        // TODO: Add resource manager
        foreach (var item in youWinRourcesText)
            item.text = 3.ToString();

        youWinRourcesText[0].text = _gold.ToString();
        youWin.SetActive(true);
        Time.timeScale = 0f;
        NoActiveObject();
    }

    private void nextLevel() {
        if (_numberLevel == PlayerPrefs.GetInt("countLevel", 1))
            PlayerPrefs.SetInt("countLevel", PlayerPrefs.GetInt("countLevel", 1) + 1);

        UserModel.singleton.gold += _gold;
        PlayerPrefs.SetInt(UserModel.RESOURCE_IRON, UserModel.singleton.GetResourceIron() + 3);
        PlayerPrefs.SetInt(UserModel.RESOURCE_WOOD, UserModel.singleton.GetResourceWood() + 3);
        PlayerPrefs.SetInt(UserModel.RESOURCE_CHARTER, UserModel.singleton.GetResourceCharter() + 3);
        SaveData.singleton.SaveToFile();
        YouWin();
    }

    public void YouWinNext() {
        DataHolder.isNext = true;
        SceneManager.LoadScene("Map");
    }

    private void NoActiveObject()
    {
        foreach (var item in noActiveObject)
            item.SetActive(!item.activeSelf);
    }

    void OnApplicationQuit() {
        SaveData.singleton.SaveToFile();
        try { PlayServiceSave.singleton.OpenSavedGame(true); }
        catch { Debug.Log("Not save cloud");}
    }
}
