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

    public void CalculateSquadRoles(SCR_AI ai)
    {

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

    public void RemoveAI(SCR_AI ai)
    {
        switch (ai.myType)
        {
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

    public void Swap(SCR_AI ai)
    {
        if (melee.Contains(ai) && ai.health <= 50)
        {
            RemoveAI(ai);
            CalculateSquadRoles(ai);
        }
    }
    public void AssignRole(SCR_AI ai, Type role)
    {
        ai.myType = role;
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
