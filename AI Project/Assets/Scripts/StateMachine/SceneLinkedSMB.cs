﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// an extension to allow the states to reference their specific ai instance
/// 
/// sourced from : https://forum.unity.com/threads/extending-statemachinebehaviours.488314/
/// </summary>
public class SceneLinkedSMB<TMonoBehaviour> : StateMachineBehaviour
    where TMonoBehaviour : MonoBehaviour
{
    protected TMonoBehaviour ai;
    bool firstFrameHappened;
    bool lastFrameHappened;

    public static void Initialise(Animator animator, TMonoBehaviour mono)
    {

        SceneLinkedSMB<TMonoBehaviour>[] sceneLinkedSMBs = animator.GetBehaviours<SceneLinkedSMB<TMonoBehaviour>>();
        for (int i = 0; i < sceneLinkedSMBs.Length; i++)
        {
            sceneLinkedSMBs[i].InternalInitialise(animator, mono);
        }        
    }

    public void InternalInitialise(Animator animator, TMonoBehaviour mono)
    {
        ai = mono;
        OnStart(animator);
    }
    public virtual void OnStart(Animator animator) { }

    public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        firstFrameHappened = false;

        OnSLStateEnter(animator, stateInfo, layerIndex);
        OnSLStateEnter(animator, stateInfo, layerIndex, controller);
    }

    public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        if (animator.IsInTransition(layerIndex) && animator.GetNextAnimatorStateInfo(layerIndex).fullPathHash == stateInfo.fullPathHash)
        {
            OnSLTransitionToStateUpdate(animator, stateInfo, layerIndex);
            OnSLTransitionToStateUpdate(animator, stateInfo, layerIndex, controller);
        }

        if (!animator.IsInTransition(layerIndex) && !firstFrameHappened)
        {
            firstFrameHappened = true;

            OnSLStatePostEnter(animator, stateInfo, layerIndex);
            OnSLStatePostEnter(animator, stateInfo, layerIndex, controller);
        }

        if (!animator.IsInTransition(layerIndex))
        {
            OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);
            OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex, controller);
        }

        if (animator.IsInTransition(layerIndex) && !lastFrameHappened && firstFrameHappened)
        {
            lastFrameHappened = true;

            OnSLStatePreExit(animator, stateInfo, layerIndex);
            OnSLStatePreExit(animator, stateInfo, layerIndex, controller);
        }

        if (animator.IsInTransition(layerIndex) && animator.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash == stateInfo.fullPathHash)
        {
            OnSLTransitionFromStateUpdate(animator, stateInfo, layerIndex);
            OnSLTransitionFromStateUpdate(animator, stateInfo, layerIndex, controller);
        }
    }

    public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        lastFrameHappened = false;

        OnSLStateExit(animator, stateInfo, layerIndex);
        OnSLStateExit(animator, stateInfo, layerIndex, controller);
    }
    public virtual void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
    
    public virtual void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public virtual void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public virtual void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public virtual void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public virtual void OnSLTransitionFromStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public virtual void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public virtual void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    public virtual void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    public virtual void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    public virtual void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    public virtual void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    public virtual void OnSLTransitionFromStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    public virtual void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
}
