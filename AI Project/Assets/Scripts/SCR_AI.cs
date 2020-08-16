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
    // Start is called before the first frame update
    void Start()
    {
        SceneLinkedSMB<SCR_AI>.Initialise(animator, this);
    }

    public bool CheckTargetDistance()
    {
        if (Vector3.Distance(target.transform.position, this.transform.position) < sightRange)
        {
            animator.SetBool("chase", true);
            return true;
        }
        else
        {
            animator.SetBool("chase", false);
            return false;
        }
    }

    public void MoveIntoRange(){
        navMeshAgent.SetDestination(target.transform.position);
        if (Vector3.Distance(target.transform.position, this.transform.position) < engagementRange)
        {
            navMeshAgent.Stop();
            animator.SetBool("Attack", true);
        } else
        {
            animator.SetBool("Attack", false);
        }
    }

    public void ChangeHealth(float amount)
    {
        health += amount;
        if (health < 10)
        {
            animator.SetBool("Flee", true);
        }
        if (health > 60)
        {
            animator.SetBool("Flee", false);
        }
    }

}
