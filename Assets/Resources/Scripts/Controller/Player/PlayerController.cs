using UnityEngine;
using Spine.Unity;
using Spine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton { get; private set;}

    public SkeletonAnimation skeletonAnimation;
    public SkeletonData skeletonData;
    public AnimationReferenceAsset idle, attacking;
    private string _currentState;
    private string _prevState;
    private string _currentAnimation;

    void Awake() { singleton = this; }

    void Start()
    {
        _currentState = "idle";
        SetCharacterState(_currentState);
    }

    public void Attack()
    {
        SetCharacterState("attack");
        Invoke("ArrowCreate", 0.5f);
    }

    private void ArrowCreate() => Bow.singleton.bowEvent.Invoke();

    public void setAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(_currentAnimation)) { return; }

        TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timeScale;
        animationEntry.Complete += AnimationEntry_Complete;
        _currentAnimation = animation.name;
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        if (_currentState.Equals("attack")) SetCharacterState("idle");
    }

    public void SetCharacterState(string state)
    {
        if (state.Equals("attack"))
            setAnimation(attacking, false, 1f);
        else
            setAnimation(idle, true, 1f);

        _currentState = state;
    }
}
