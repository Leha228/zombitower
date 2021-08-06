using UnityEngine;

public class ForgeModel : MonoBehaviour
{
    public static ForgeModel singleton { get; private set; }

    [SerializeField] public GameObject[] resourceList;

    private void Awake() {
        singleton = this;
    }
}
