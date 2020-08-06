using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Turret : MonoBehaviour
{
    public MeshRenderer renderer;
    public enum states
    {
        inActive,
        search,
        lost,
        attack,
        destroy
    };
    private states myState;

    bool shooting;
    public float fireRate;
    public states newState{
        get { return myState;}

        set { 
            myState = value;
            switch (myState)
            {
                case states.inActive:
                    renderer.material.color = Color.white;
                    break;
                case states.search:
                    renderer.material.color = Color.blue;
                    break;
                case states.lost:
                    renderer.material.color = Color.green;
                    break;
                case states.attack:
                    renderer.material.color = Color.red;
                    break;
                case states.destroy:
                    renderer.material.color = Color.grey;
                    break;
            }
        }
    }

    public List<GameObject> enemiesInRange;

    public float lostDelay = 10;
    private float lostTime;

    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<GameObject>();
        renderer = this.gameObject.GetComponent<MeshRenderer>();
        newState = states.inActive;
    }

    // Update is called once per frame
    void Update()
    {
        switch (myState)
        {
            case states.inActive:
                break;
            case states.search:
                Search();
                break;
            case states.lost:
                Lost();
                break;
            case states.attack:
                Attack();
                break;
            case states.destroy:
                break;
        }
    }

    /// <summary>
    /// check field of view for player
    /// calculate if player is in front of the turrt
    /// if player can be seen change to attack
    /// </summary>
    public void Search()
    {
        if (enemiesInRange.Count <= 0)
        {
            newState = states.lost;
            lostTime = lostDelay;
        }
    }

    public void Lost()
    {
        lostTime -= Time.deltaTime;
        if (lostTime <= 0)
        {
            newState = states.inActive;
        }
    }

    /// <summary>
    /// attack player
    /// start to fire at player intermittenly
    /// </summary>
    
    public void Attack()
    {
        if (!shooting)
        {
            shooting = true;
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        new WaitForSeconds(fireRate * 3);

        //instansiate bullet
        new WaitForSeconds(fireRate);
        //instansiate bullet
        new WaitForSeconds(fireRate);
        //instansiate bullet

        shooting = false;
        yield return null;
    }

    /// <summary>
    /// when player moves into detection range then change to search
    /// </summary>
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            enemiesInRange.Add(other.gameObject);
            if (newState == states.inActive || newState == states.lost)
            {
                newState = states.search;
            }
        }
    }
    /// <summary>
    /// if player moves away from detection range change to lost state
    /// </summary>
    public void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            enemiesInRange.Remove(other.gameObject);           
        }
    }
}
