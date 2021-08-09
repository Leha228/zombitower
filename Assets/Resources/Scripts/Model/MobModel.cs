using System.Collections;
using UnityEngine;

public class MobModel : MonoBehaviour
{
    public static MobModel singleton { get; private set; }

    [SerializeField] public GameObject[] collisions;

    private void Awake() { singleton = this; }
}
