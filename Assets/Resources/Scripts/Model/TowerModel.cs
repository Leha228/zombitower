using UnityEngine;
using Spine.Unity;
using Spine;

public class TowerModel : MonoBehaviour
{
    public static TowerModel singleton { get; private set; }

    [SerializeField] public SkeletonAnimation skeletonAnimation;
    [SerializeField] public SkeletonData skeletonData;
    [SerializeField] public AnimationReferenceAsset idle, aiming, attack, open, close;

    private void Awake() {
        singleton = this;
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop) 
        => skeletonAnimation.state.SetAnimation(0, animation, loop);
}
