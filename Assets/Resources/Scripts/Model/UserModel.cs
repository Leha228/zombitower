using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserModel : MonoBehaviour
{
    public static UserModel singleton { get; private set; }

    [SerializeField] public Text[] cashList;
    [SerializeField] public Image[] resourceList;
    [SerializeField] public int diamond;
    [SerializeField] public int gold;
    [SerializeField] public List<int> towers;
    [SerializeField] public List<int> players;

    public const string DIAMOND = "diamond";
    public const string GOLD = "gold";
    public const string ACTIVE_TOWER = "active_tower";
    public const string ACTIVE_PLAYER = "active_player";
    public const string RESOURCE_WOOD = "resource_wood";
    public const string RESOURCE_IRON = "resource_iron";
    public const string RESOURCE_CHARTER = "resource_charter";
    public const string ACTIVE = "Active";
    public const string SELECT = "Select";
    public const string PAYMENT = "Payment";
    public const string ARROWS = "Arrows";


    private void Awake() {
        singleton = this;
    }

    public int GetDiamond() => diamond;
    public int GetGold() => gold;
    public int GetActiveTower() => PlayerPrefs.GetInt(ACTIVE_TOWER, 0);
    public int GetActivePlayer() => PlayerPrefs.GetInt(ACTIVE_PLAYER, 0);
    public int GetResourceWood() => PlayerPrefs.GetInt(RESOURCE_WOOD, 0);
    public int GetResourceIron() => PlayerPrefs.GetInt(RESOURCE_IRON, 0);
    public int GetResourceCharter() => PlayerPrefs.GetInt(RESOURCE_CHARTER, 0);
    public int GetArrows() => PlayerPrefs.GetInt(ARROWS, 0);
}
