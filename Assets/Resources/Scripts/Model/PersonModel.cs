using UnityEngine;

public class PersonModel : MonoBehaviour
{
    public static PersonModel singleton {get; private set;}

    [SerializeField] public GameObject[] towerList;
    [SerializeField] public GameObject[] towerSelectList;

    private void Awake() { singleton = this; }
}
