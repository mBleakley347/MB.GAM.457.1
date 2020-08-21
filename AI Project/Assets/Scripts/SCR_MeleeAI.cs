using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// redundant with current method
/// </summary>
public class SCR_MeleeAI : MonoBehaviour
{
    public SCR_SquadManager.Type myType;

    public Animator animator;

    public float sightRange = 10;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        SceneLinkedSMB<SCR_MeleeAI>.Initialise(animator, this);
    }

    public void Idle()
    {
        if (Vector3.Distance(target.transform.position, this.transform.position) < sightRange)
        {
            animator.SetBool("chase", true);
        }
    }
}
