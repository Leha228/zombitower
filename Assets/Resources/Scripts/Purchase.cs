using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchase : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {
        UserModel.singleton.diamond += 100;
        SaveData.singleton.SaveToFile();
    }

    public void OnPurchaseFail(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase fail");
    }
}
