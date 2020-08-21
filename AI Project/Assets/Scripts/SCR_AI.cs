using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SCR_AI : MonoBehaviour
{
    public SCR_SquadManager.Type myType;
    public NavMeshAgent navMeshAgent;

    public Animator animator;

    public float health = 100;

    public float sightRange = 10;
    public float engagementRange;

    public GameObject target;

    public MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        // set up extensions in the state machine
        SceneLinkedSMB<SCR_AI>.Initialise(animator, this);
    }

    /// <summary>
    /// check if target(player) is within sight range
    /// </summary>
    public bool CheckTargetDistance()
    {
        if (Vector3.Distance(target.transform.position, this.transform.position) < sightRange)
        {
            //change state to chase if in sight
            animator.SetBool("chase", true);
            return true;
        }
        else
        {
            //change off chase if not in range
            animator.SetBool("chase", false);
            return false;
        }
    }

    /// <summary>
    /// move toward target(player)
    /// </summary>
    public void MoveIntoRange(){
        navMeshAgent.SetDestination(target.transform.position);
        CheckRange();
    }

    /// <summary>
    /// check if ai is within its engagement range and if it is too close
    /// </summary>
    public void CheckRange()
    {
        // move backward if too close to target
        if (Vector3.Distance(target.transform.position, this.transform.position) < engagementRange - 1)
        {
            navMeshAgent.SetDestination((this.transform.position * 2 - target.transform.position));
            return;
        }
        // stop moving if in correct position and change to attack state
        if (Vector3.Distance(target.transform.position, this.transform.position) < engagementRange)
        {
            navMeshAgent.SetDestination(this.transform.position);
            animator.SetBool("Attack", true);
        }
        else
        {
            // make sure attack state is not active
            animator.SetBool("Attack", false);
        }
    }

    /// <summary>
    /// change the health of the ai when damages and check if ai is still alive or able to continue fighting
    /// </summary>
    public void ChangeHealth(float amount)
    {
        health += amount;
        //if below health threshold of 10 start to flee
        if (health < 10)
        {
            animator.SetBool("Flee", true);
            animator.SetBool("chase", false);
        }
        //if returned above 60 stop fleeing
        if (health > 60)
        {
            animator.SetBool("Flee", false);
        }
        //if a melee in squad try to get swapped with another ai 
        if (myType == SCR_SquadManager.Type.Melee)
        {
            SCR_SquadManager.instance.Swap(this);
        }
    }

    /// <summary>
    /// move away from target
    /// </summary>
    public void Flee()
    {
        //Debug.Log(this.transform.position + (target.transform.position - this.transform.position));
        // check if being followed and attack if so
        CheckRange();
        // move away from target
        navMeshAgent.SetDestination((this.transform.position * 2 - target.transform.position));
    }

    /// <summary>
    /// didnt realy need anything here just place holder
    /// </summary>
    public void Attack()
    {
    }
}
