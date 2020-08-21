using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SCR_Attack : SceneLinkedSMB<SCR_AI>
{
    /// <summary>
    /// Reference to Attack code on Ai Script
    /// </summary>
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        //visual change to indicate state change
        ai.renderer.material.color = Color.red;
        ai.Attack();
    }
}
