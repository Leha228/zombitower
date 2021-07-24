using UnityEngine;

public class MobModel : MonoBehaviour
{
    public static MobModel singleton { get; private set; }

    [SerializeField] public GameObject knight;

    private void Awake() { singleton = this; }
}
