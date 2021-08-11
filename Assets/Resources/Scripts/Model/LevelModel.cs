using UnityEngine;

public class LevelModel : MonoBehaviour
{
    public static LevelModel singleton { get; private set; }

    [SerializeField] public GameObject[] enemyList;
    [SerializeField] public int gold;

    private void Awake() {
        singleton = this;
    }
}
