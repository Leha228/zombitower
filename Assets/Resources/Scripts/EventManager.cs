using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager singleton { get; private set; }
    [SerializeField] int countEnemy = 3;
    [SerializeField] GameObject menu;
    public int progress = 0;
    private bool pause = false;

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
        progress = (int)(100 / countEnemy) + progress;
        if (progress >= 99) Debug.Log("Win"); 
    }

    public void OnMenu() {
        pause = !pause;
        menu.SetActive(pause);
    }
}
