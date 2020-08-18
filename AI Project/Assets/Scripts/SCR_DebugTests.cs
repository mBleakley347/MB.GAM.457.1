using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_DebugTests : MonoBehaviour
{
    [Header("Deal Damage")]
    public SCR_AI target;
    public KeyCode binding = KeyCode.E;
    public int amount;

    public void Update()
    {
        if (Input.GetKeyDown(binding)) DealDamage();
    }
    public void DealDamage()
    {
        target.ChangeHealth(-amount);
    }

}
