using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager singleton { get; private set; }

    [SerializeField] public GameObject menu;
    public int progress = 0;
    private int _numberLevel = DataHolder.numberLevel;
    private int _countEnemy = DataHolder.listEnemy.Length;

    private void Awake() { singleton = this; }

    public void CreateMob() {
        OpenGate();

        GameObject enemyCopy = Instantiate(MobModel.singleton.knight);
        enemyCopy.transform.position = MobModel.singleton.transform.position;

        Invoke("CloseGate", 2f);
    }

    private void OpenGate() => TowerModel.singleton.SetAnimation(TowerModel.singleton.open, false); 
    private void CloseGate() => TowerModel.singleton.SetAnimation(TowerModel.singleton.close, false); 

    public void SetProgress() {
        progress = (int)(100 / _countEnemy) + progress;
        if (progress >= 99) Debug.Log("Win"); 
    }

    public void OnMenu() {
        menu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnResume() {
        menu.SetActive(false);
        Time.timeScale = 1f;
    }
}
