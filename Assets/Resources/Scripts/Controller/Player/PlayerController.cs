using UnityEngine;
using Spine.Unity;
using Spine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton { get; private set;}

    [SerializeField] public SkeletonAnimation skeletonAnimation;
    [SerializeField] public SkeletonData skeletonData;
    [SerializeField] public AnimationReferenceAsset[] idle;
    [SerializeField] public AnimationReferenceAsset[] attacking;
    [SerializeField] public SkeletonDataAsset[] dataAsset;
    [SerializeField] private Image healthBar;
    [SerializeField] public int[] live;
    private string _currentState;
    private string _prevState;
    private string _currentAnimation;
    private float partLive;

    void Awake() { singleton = this; }

    void Start()
    {
        skeletonAnimation.skeletonDataAsset = dataAsset[UserModel.singleton.GetActiveHerous()];
        skeletonAnimation.Initialize(true);

        _currentState = "idle";
        SetCharacterState(_currentState);
        InitHealthBar();
    }

    public void Attack()
    {
        SetCharacterState("attack");
        Invoke("ArrowCreate", 0.5f);
    }

    private void ArrowCreate() => Bow.singleton.bowEvent.Invoke();

    private void InitHealthBar()
    {
        healthBar.fillAmount = 1f;
        partLive = (live[UserModel.singleton.GetActiveHerous()] / TowerModel.singleton.damage[UserModel.singleton.GetActiveTower()]);
        partLive = 100 / partLive;
        partLive = partLive / 100;
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
        if (_currentState.Equals("attack")) SetCharacterState("idle");
    }

    public void SetCharacterState(string state)
    {
        if (state.Equals("attack"))
            setAnimation(attacking[UserModel.singleton.GetActiveHerous()], false, 1f);
        else
            setAnimation(idle[UserModel.singleton.GetActiveHerous()], true, 1f);

        _currentState = state;
    }
}
