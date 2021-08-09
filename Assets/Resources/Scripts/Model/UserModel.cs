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

    public const string DIAMOND = "diamond";
    public const string GOLD = "gold";
    public const string ACTIVE_TOWER = "active_tower";
    public const string RESOURCE_WOOD = "resource_wood";
    public const string RESOURCE_IRON = "resource_iron";
    public const string RESOURCE_CHARTER = "resource_charter";
    public const string ACTIVE = "Active";
    public const string SELECT = "Select";


    private void Awake() {
        singleton = this;
    }

    public int GetDiamond() => PlayerPrefs.GetInt(DIAMOND, 0);
    public int GetGold() => PlayerPrefs.GetInt(GOLD, 0);
    public int GetActiveTower() => PlayerPrefs.GetInt(ACTIVE_TOWER, 0);
    public int GetResourceWood() => PlayerPrefs.GetInt(RESOURCE_WOOD, 0);
    public int GetResourceIron() => PlayerPrefs.GetInt(RESOURCE_IRON, 0);
    public int GetResourceCharter() => PlayerPrefs.GetInt(RESOURCE_CHARTER, 0);

}
