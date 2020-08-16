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
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
