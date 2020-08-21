using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SCR_Idle : SceneLinkedSMB<SCR_AI>
{
    /// <summary>
    /// tell ai to check if target is in range
    /// </summary>
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        ai.renderer.material.color = Color.white;
        ai.CheckTargetDistance();
    }

    /// <summary>
    /// remove ai from squad manager is in squad manager
    /// </summary>
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        if (SCR_SquadManager.instance.aiList.Contains(ai))
        {
            SCR_SquadManager.instance.aiList.Remove(ai);
            SCR_SquadManager.instance.RemoveAI(ai);
        }
    }
}
