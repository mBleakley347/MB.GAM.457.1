using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SCR_Flee : SceneLinkedSMB<SCR_AI>
{
    /// <summary>
    /// tell Ai to Fless
    /// </summary>
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        ai.renderer.material.color = Color.black;
        ai.Flee();
    }
    /// <summary>
    /// tell squad manager that this ai is no longer in the squad
    /// </summary>
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        //make sure the ai is already in the squad before removing it
        if (SCR_SquadManager.instance.aiList.Contains(ai))
        {
            SCR_SquadManager.instance.aiList.Remove(ai);
            SCR_SquadManager.instance.RemoveAI(ai);
        }
    }
}
