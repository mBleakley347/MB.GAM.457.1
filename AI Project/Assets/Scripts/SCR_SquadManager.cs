using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_SquadManager : MonoBehaviour
{
    public static SCR_SquadManager instance;

    public enum Type
    {
        Melee,
        Ranged,
        Blocking
    };

    public float meleeRange;
    public float rangedRange;
    public float blockingRange;
    public List<SCR_AI> aiList;
    public List<SCR_AI> melee;
    public List<SCR_AI> ranged;
    public List<SCR_AI> block;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// calculate the role the ai should play
    /// </summary>
    public void CalculateSquadRoles(SCR_AI ai)
    {
        // only ai with health above 50 get to be melee
        if (ai.health > 50)
        {
            if (melee.Count <= block.Count)
            {
                AssignRole(ai, Type.Melee);
                return;
            } 
        }
        if (ranged.Count <= block.Count)
        {
            AssignRole(ai, Type.Ranged);
        }
        else
        {
            AssignRole(ai, Type.Blocking);
        }
    }

    /// <summary>
    /// removes specific ai from the squads roster and checks if they should be replaced 
    /// </summary>
    public void RemoveAI(SCR_AI ai)
    {
        switch (ai.myType)
        {
            // if a melee is removed try to replace it with another ai
            case Type.Melee:
                melee.Remove(ai);
                if (block.Count >= 1)
                {
                    AssignRole(block[0], Type.Melee);
                    block.RemoveAt(0);
                }
                else if(ranged.Count >= 1)
                {
                    AssignRole(ranged[0], Type.Melee);
                    ranged.RemoveAt(0);
                }
                break;
            case Type.Ranged:
                ranged.Remove(ai);
                break;
            case Type.Blocking:
                block.Remove(ai);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// check if the ai need to be swapped out to a different roles
    /// </summary>
    public void Swap(SCR_AI ai)
    {
        if (melee.Contains(ai) && ai.health <= 50)
        {
            //removes ai from its current list and replaces them
            RemoveAI(ai);
            //recalculates the role of this ai
            CalculateSquadRoles(ai);
        }
    }

    /// <summary>
    /// assigne the specific role to the ai
    /// </summary>
    public void AssignRole(SCR_AI ai, Type role)
    {
        ai.myType = role;
        // put the ai into the list of their role and set their engagement range
        switch (role)
        {
            case Type.Melee:
                melee.Add(ai);
                ai.engagementRange = meleeRange;
                break;
            case Type.Ranged:
                ranged.Add(ai);
                ai.engagementRange = rangedRange;
                break;
            case Type.Blocking:
                block.Add(ai);
                ai.engagementRange = blockingRange;
                break;
            default:
                break;
        }
    }
}
