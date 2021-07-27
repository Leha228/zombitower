using UnityEngine;

public class LevelModel : MonoBehaviour
{
    public static LevelModel singleton { get; private set; }
    [SerializeField] public GameObject[] listEnemy;

    private void Awake() {
        singleton = this;
    }
}
