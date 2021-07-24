using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class TowerModel : MonoBehaviour
{
    public static TowerModel singleton { get; private set; }

    [SerializeField] public SkeletonAnimation skeletonAnimation;
    [SerializeField] public SkeletonData skeletonData;
    [SerializeField] public AnimationReferenceAsset idle, aiming, attack, open, close;
    [SerializeField] public int live;
    private List<string> _collisions;
    private bool _coroutine = false;

    private void Awake() {
        singleton = this;
        _collisions = new List<string> {
            "zombie_great(Clone)"
        };
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop) 
        => skeletonAnimation.state.SetAnimation(0, animation, loop);

    void OnCollisionStay2D(Collision2D other) {
         if (!_collisions.Contains(other.collider.name)) return;
         if (other.collider.name == _collisions[0]) {
             if (!_coroutine) StartCoroutine("Damage", 1f);
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
    }
}
