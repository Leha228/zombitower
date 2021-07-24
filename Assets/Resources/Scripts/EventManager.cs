using UnityEngine;

public class EventManager : MonoBehaviour
{
    public void CreateMob() {
        OpenGate();

        GameObject enemyCopy = Instantiate(MobModel.singleton.knight);
        enemyCopy.transform.position = MobModel.singleton.transform.position;

        Invoke("CloseGate", 2f);
    }

    private void OpenGate() => TowerModel.singleton.SetAnimation(TowerModel.singleton.open, false); 
    private void CloseGate() => TowerModel.singleton.SetAnimation(TowerModel.singleton.close, false); 
}
