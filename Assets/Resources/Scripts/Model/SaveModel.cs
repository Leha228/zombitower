using UnityEngine;

[System.Serializable]
public class SaveModel : MonoBehaviour 
{
    public static SaveModel singleton {get; private set;}

    public int gold;
    public int diamond;
    public int[] towers;

    private void Awake() { singleton = this; }
}
