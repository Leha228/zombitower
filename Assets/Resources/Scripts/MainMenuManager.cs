using UnityEngine;
using Spine.Unity;
using Spine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager singleton { get; private set; }
    [SerializeField] private SkeletonDataAsset[] dataAssetTower;
    [SerializeField] private SkeletonAnimation tower;
    [SerializeField] private SkeletonDataAsset[] dataAssetHerous;
    [SerializeField] private SkeletonAnimation herous;
    [SerializeField] private GameObject[] settingOpenClose;

    private void Awake() { singleton = this; }

    private void Start() => RenameDataAsset();

    public void RenameDataAsset()
    {
        tower.skeletonDataAsset = dataAssetTower[UserModel.singleton.GetActiveTower()];
        herous.skeletonDataAsset = dataAssetHerous[UserModel.singleton.GetActiveHerous()];
        tower.Initialize(true);
        herous.Initialize(true);
    }

    public void SettingOpenClose() {
        foreach (var item in settingOpenClose)
            item.SetActive(!item.activeSelf);
    }
}
