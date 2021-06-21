using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
using System;

public class PlayerController : MonoBehaviour
{

    public SkeletonAnimation skeletonAnimation;
    public SkeletonData skeletonData;
    public AnimationReferenceAsset idle, attacking, aiming, aiming1;
    private string currentState;
    private string prevState;
    private string currentAnimation;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = "idle";

        setCharacterState(currentState);
    }

    void Update()
    {

    }

    public void attack()
    {
        setCharacterState("attack_aim");
    }

    public void aim()
    {
        if (currentState.Equals("aim1")) return;
        if (!currentState.Equals("aim")) { prevState = currentState; }
        setCharacterState("aim");
    }

    public void setAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation)) { return; }

        TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timeScale;
        animationEntry.Complete += AnimationEntry_Complete;
        currentAnimation = animation.name;
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        if (currentState.Equals("jump")) { setCharacterState(prevState); }
        if (currentState.Equals("attack_aim")) { setCharacterState("idle"); }
        if (currentState.Equals("aim")) { setCharacterState("aim1"); }
    }

    public void setCharacterState(string state)
    {
        if (state.Equals("aim"))
            setAnimation(aiming, false, 1f);
        else if (state.Equals("aim1"))
            setAnimation(aiming1, false, 1f);
        else if (state.Equals("attack_aim"))
            setAnimation(attacking, false, 1f);
        else
            setAnimation(idle, true, 1f);

        currentState = state;
    }
}
