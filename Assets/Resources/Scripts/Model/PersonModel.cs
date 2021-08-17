using UnityEngine;

public class PersonModel : MonoBehaviour
{
    public static PersonModel singleton {get; private set;}

    [SerializeField] public GameObject[] tabs;
    [SerializeField] public GameObject[] towerList;
    [SerializeField] public GameObject[] towerSelectList;
    [SerializeField] public GameObject[] playerList;
    [SerializeField] public GameObject[] playerSelectList;

    private void Awake() { singleton = this; }
}
