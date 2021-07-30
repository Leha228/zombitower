using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static EventManager singleton { get; private set; }

    [SerializeField] public GameObject menu;
    [SerializeField] public GameObject youDie;
    [SerializeField] public GameObject youDieContinue;
    private int _progress = 0;
    private int _numberLevel = DataHolder.numberLevel;
    private int _countEnemy = DataHolder.enemyList.Length;

    private void Awake() { singleton = this; }

    private void OpenGate() => TowerModel.singleton.SetAnimation(TowerModel.singleton.open, false); 
    private void CloseGate() => TowerModel.singleton.SetAnimation(TowerModel.singleton.close, false); 
    public int GetProgress() => _progress;
    public void YouDieContinueOpen() => youDieContinue.SetActive(true);
    public void YouDieContinueClose() => youDieContinue.SetActive(false);
    public void YouDieExit() => SceneManager.LoadScene("Map");
    public void YouDieRestart() => SceneManager.LoadScene("Game");

    public void CreateMob() {
        OpenGate();

        GameObject enemyCopy = Instantiate(MobModel.singleton.knight);
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
    }

    public void OnResume() {
        menu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void YouDie() {
        youDie.SetActive(true);
        Time.timeScale = 0f;
    }

    private void nextLevel() {
        if (_numberLevel == PlayerPrefs.GetInt("countLevel", 1))
            PlayerPrefs.SetInt("countLevel", PlayerPrefs.GetInt("countLevel", 1) + 1);
        SceneManager.LoadScene("Map");
    }
}
