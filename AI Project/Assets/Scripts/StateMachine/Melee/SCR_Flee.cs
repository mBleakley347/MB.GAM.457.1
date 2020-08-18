using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SCR_Flee : SceneLinkedSMB<SCR_AI>
{
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        ai.renderer.material.color = Color.black;
        ai.Flee();
    }
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        if (SCR_SquadManager.instance.aiList.Contains(ai))
        {
            SCR_SquadManager.instance.aiList.Remove(ai);
            SCR_SquadManager.instance.RemoveAI(ai);
        }
    }
}
