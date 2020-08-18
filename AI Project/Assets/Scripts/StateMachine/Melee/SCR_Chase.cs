﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SCR_Chase : SceneLinkedSMB<SCR_AI>
{
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        ai.renderer.material.color = Color.cyan;
        if (!ai.CheckTargetDistance())
        {
            return;
        }
        ai.MoveIntoRange();
    }

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        if (!SCR_SquadManager.instance.aiList.Contains(ai))
        {
            SCR_SquadManager.instance.aiList.Add(ai);
            SCR_SquadManager.instance.CalculateSquadRoles(ai);
        }
    }
    
}
