using UnityEngine;
using System.Collections.Generic;
using Spine.Unity;
using Spine;

public class KnightModel : MonoBehaviour
{
    public static KnightModel singleton { get; private set; }

    [SerializeField] public SkeletonAnimation skeletonAnimation;
    [SerializeField] public SkeletonData skeletonData;
    [SerializeField] public AnimationReferenceAsset idle, walk, attack, death, jump, r_fun, r_sad, create;
    [SerializeField] public float speed;

    public List<string> collisions;

    private void Awake() { 
        singleton = this; 
        collisions = new List<string> {
            "zombie_great(Clone)",
        };
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop) {
        skeletonAnimation.state.SetAnimation(1, animation, loop);
        skeletonAnimation.timeScale = 1f;
    }
    
}
