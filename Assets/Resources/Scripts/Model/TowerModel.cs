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
    [SerializeField] public GameObject[] pointShoot;
    [SerializeField] public int live;
    private List<string> _collisions;
    private bool _coroutine = false;

    private void Awake() {
        singleton = this;
        _collisions = new List<string> {
            "zombie_great(Clone)", 
            "zombie_simple(Clone)"
        };
    }

    private void Start() {
        skeletonAnimation.skeletonDataAsset = dataAsset[UserModel.singleton.GetActiveTower()];
        skeletonAnimation.Initialize(true);
        UpdatePointShoot();
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop) 
        => skeletonAnimation.state.SetAnimation(0, animation, loop);

    private void OnCollisionStay2D(Collision2D other) {
         if (!_collisions.Contains(other.collider.name)) return;
         if (other.collider.name == _collisions[0]) {
             if (!_coroutine) StartCoroutine("Damage", 1f);
         }
    }

    private void CheckDamage() {
        if (live < 1) 
            EventManager.singleton.YouDie();
    }

    private void UpdatePointShoot() {

        foreach (var item in pointShoot)
        {
            if (item.name == shells[UserModel.singleton.GetActiveTower()].name) 
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    IEnumerator Damage(float timeSecond) {
        float counter = 0;
        _coroutine = true;

        while (counter < timeSecond) {
            counter += Time.deltaTime;
            yield return null;
        }

        _coroutine = false;
        live -= 10;
        CheckDamage();
    }
}
