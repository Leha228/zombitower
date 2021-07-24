using UnityEngine;
using Spine.Unity;
using Spine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton { get; private set;}

    public SkeletonAnimation skeletonAnimation;
    public SkeletonData skeletonData;
    public AnimationReferenceAsset idle, attacking, aiming, aiming1;
    private string _currentState;
    private string _prevState;
    private string _currentAnimation;
    private Rigidbody2D rb;

    void Awake() { singleton = this; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _currentState = "idle";

        SetCharacterState(_currentState);
    }

    public void Attack()
    {
        SetCharacterState("attack_aim");
    }

    public void Aim()
    {
        if (_currentState.Equals("aim1")) return;
        if (!_currentState.Equals("aim")) { _prevState = _currentState; }
        SetCharacterState("aim");
    }

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
        if (_currentState.Equals("jump")) { SetCharacterState(_prevState); }
        if (_currentState.Equals("attack_aim")) { SetCharacterState("idle"); }
        if (_currentState.Equals("aim")) { SetCharacterState("aim1"); }
    }

    public void SetCharacterState(string state)
    {
        if (state.Equals("aim"))
            setAnimation(aiming, false, 1f);
        else if (state.Equals("aim1"))
            setAnimation(aiming1, false, 1f);
        else if (state.Equals("attack_aim"))
            setAnimation(attacking, false, 1f);
        else
            setAnimation(idle, true, 1f);

        _currentState = state;
    }
}
