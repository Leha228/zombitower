using UnityEngine;

public class ShopModel : MonoBehaviour
{
    public static ShopModel singleton { get; private set; }
    
    [SerializeField] public GameObject shop;
    [SerializeField] public GameObject[] tabs;

    private void Awake() { singleton = this; }
}
