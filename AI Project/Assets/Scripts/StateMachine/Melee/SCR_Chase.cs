using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SCR_Chase : SceneLinkedSMB<SCR_AI>
{
    /// <summary>
    /// references to code on AI Script
    /// call distance check
    /// call move
    /// </summary>
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        ai.renderer.material.color = Color.cyan;
        if (!ai.CheckTargetDistance())
        {
            return;
        }
        ai.MoveIntoRange();
    }

    /// <summary>
    /// adds ai to squad manager and tell squad to calculate this AIs role
    /// </summary>
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        // make sure this ai is not already in the squad
        if (!SCR_SquadManager.instance.aiList.Contains(ai))
        {
            SCR_SquadManager.instance.aiList.Add(ai);
            SCR_SquadManager.instance.CalculateSquadRoles(ai);
        }
    }
    
}
