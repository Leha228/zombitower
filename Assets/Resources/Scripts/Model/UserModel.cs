using UnityEngine;
using UnityEngine.UI;

public class UserModel : MonoBehaviour
{
    public static UserModel singleton { get; private set; }
    [SerializeField] public Text[] cashList;
    [SerializeField] public int diamond;
    [SerializeField] public int gold;


    private void Awake() {
        singleton = this;
    }
}
