using UnityEngine;

public class LevelModel : MonoBehaviour
{
    public static LevelModel singleton { get; private set; }
    [SerializeField] public GameObject[] enemyList;

    private void Awake() {
        singleton = this;
    }
}
