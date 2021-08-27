using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;

public class TowerModel : MonoBehaviour
{
    public static TowerModel singleton { get; private set; }

    [SerializeField] public SkeletonAnimation skeletonAnimation;
    [SerializeField] public SkeletonData skeletonData;
    [SerializeField] public AnimationReferenceAsset[] idle;
    [SerializeField] public AnimationReferenceAsset[] aiming;
    [SerializeField] public AnimationReferenceAsset[] attack;
    [SerializeField] public AnimationReferenceAsset[] open;
    [SerializeField] public AnimationReferenceAsset[] close;
    [SerializeField] public GameObject[] shells;
    [SerializeField] public GameObject[] mobs;
    [SerializeField] public int[] damage;
    [SerializeField] public SkeletonDataAsset[] dataAsset;
    [SerializeField] public GameObject[] pointButtonShoot;
    [SerializeField] public int live;
    private List<string> _collisions;
    private bool _coroutine = false;

    private void Awake() {
        singleton = this;
        _collisions = new List<string> {
            "zombie_great(Clone)",
            "zombie_simple(Clone)"
        };
        UpdatePointShoot();
    }

    private void Start() {
        skeletonAnimation.skeletonDataAsset = dataAsset[UserModel.singleton.GetActiveTower()];
        skeletonAnimation.Initialize(true);
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop)
        => skeletonAnimation.state.SetAnimation(0, animation, loop);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_collisions.Contains(collision.collider.name)) return;

        live -= 1;
        Invoke("CheckDamage", 2f); 
    }

    private void CheckDamage() {
        if (live < 1)
            EventManager.singleton.YouDie();
    }

    private void UpdatePointShoot() {
        foreach (var item in pointButtonShoot)
        {
            if (item.name == shells[UserModel.singleton.GetActiveTower()].name)
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }
}
