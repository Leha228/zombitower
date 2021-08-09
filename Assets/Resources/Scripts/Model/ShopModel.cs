using UnityEngine;
using UnityEngine.UI;

public class ShopModel : MonoBehaviour
{
    public static ShopModel singleton { get; private set; }
    
    [SerializeField] public GameObject shop;
    [SerializeField] public GameObject[] tabs;
    [SerializeField] public Button[] towers;
    //[SerializeField] public GameObject[] herous;
    //[SerializeField] public GameObject[] shells;
    //[SerializeField] public GameObject[] vip;

    private void Awake() { singleton = this; }
}
