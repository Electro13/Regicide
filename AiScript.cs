using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiScript : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent navMeshAgent;
    public float EnemyDistanceRun;
    public List<GameObject> daCrips = new List<GameObject>();

    [SerializeField]
    float timer;

    [SerializeField]
    bool PullUp;

    [SerializeField]
    float distanceToPlayer;

    [SerializeField]
    int AttackValue;

    public bool TakenHit;

    [Header("Caps")]
    // caps are needed because I said so
    // if Attack value exceeds this number, don't go higher
    public int positiveCap = 25;
    // if the Attack value goes lower than this number, don't go lower
    public int negitiveCap = -10;
    // if the player gets this far away, return to Wander state
    public float distanceCap = 40f;

    [Header("Values")]
    // value for controlling when to attack the player, default value: 10
    public int runDownValue = 10;

    // value for controlling when to run away from the player, default value: -5
    public int runAwayValue = -5;

    // value for controlling how much getting hit affects the value, default value: -10
    public int HitTakenValue = -10;

    // value for controlling how much the player getting hit affects the value, default value: TBD
    public int PlayerDamagedValue = -0;

    // value for determining if the player is 'close', default value: 6
    public int closenessProx = 6;

    // value for determining if the player is 'close', default value: 6
    public int tooCloseValue = 10;

    // value for determining if the player is 'far', default value: 15
    public int farEnoughValue = 15;



    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        permValue();
    }

    // Update is called once per frame
    void Update()
    {
        //we always want the distance to the player and other enemies
        float dist = Vector3.Distance(transform.position, target.transform.position);
        distanceToPlayer = dist;

        foreach (GameObject crips in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector3.Distance(transform.position, crips.transform.position) < 10f)
            {
                if (!daCrips.Contains(crips)) daCrips.Add(crips);
            }
            else daCrips.Remove(crips);
        }
        for (int i = 0; i < daCrips.Count; i++)
        {
            if (daCrips[i] == null) daCrips.Remove(daCrips[i]);
        }

        timer -= Time.deltaTime;
        if (timer < -1f) timer = -1f;

        // wanted to add a value to determine how the ai reacts rather than the amount of enemies around it
        // thought it would be easier to completely redo
        #region code before AttackValue, obsolete

        /*if (dist >= 20f) SightSee = true;
        else SightSee = false;

        //check how much is in list
        if (daCrips.Count >= 4) PullUp = true;
        else PullUp = false;

        //if there's less than the threshold of allied enemies around this singuluar enemy, but more than itself, it'll move closer to group up
        // just chooses the closest one
        if (daCrips.Count >= 1 && daCrips.Count < 4 && !SightSee) navMeshAgent.SetDestination(daCrips[0].transform.position);

        // check distance to player and runaway if these conditions are met
        if (dist < EnemyDistanceRun && !PullUp) RunAway();
        // otherwise jump the player
        else if (dist < EnemyDistanceRun && PullUp) RunDown();

        //for the sake of performance, if the player is really far just don't calculate movement
        if (dist > 50f) navMeshAgent.isStopped = true;
        else navMeshAgent.isStopped = false;*/

        #endregion

        // for the sake of a clean update method the bread and butter of the code is in this function
        DecisionMaking();
    }

    // A bool to tell if the player is close
    bool playerClose;
    void DecisionMaking()
    {
        // formula used to determine the attack value, caps out at 25 and -10
        // the cap is arbitrary and can be changed if needed
        formula();
        if (AttackValue >= positiveCap) AttackValue = positiveCap;
        else if (AttackValue <= negitiveCap) AttackValue = negitiveCap;

        // if the player is too far from the NPC they go back to their default state
        if (distanceToPlayer > 40f)
        {
            AttackValue = 0;
            Wander();
        }

        if (AttackValue >= runDownValue) RunDown();
        if (AttackValue <= runAwayValue) RunAway();

        // things that dont really change, or is default
        #region perm effectors, unneeded at the moment
        /*if (distanceToPlayer < 10f && !playerClose)
        {
            AttackValue = +1;
            playerClose = true;
        }
        else if (distanceToPlayer > 10f && playerClose)
        {
            AttackValue = -1;
            playerClose = false;
        }
        else return;*/
        #endregion
    }

    int HitTaken()
    {
        if (TakenHit)
        {
            return HitTakenValue;
        }
        else return 0;
    }

    int PlayerDamaged()
    {
        return PlayerDamagedValue;
    }

    int PlayerDistance()
    {
        int PlayerDist = Mathf.FloorToInt(distanceToPlayer);
        if (PlayerDist < closenessProx)
        {
            return -PlayerDist / tooCloseValue;
        }
        else return PlayerDist - farEnoughValue;
    }

    // exact same code as RunAway, but i declear it differently for the time being
    // this will be removed and heanceforth just use RunAway();
    void StayADistance()
    {
        Vector3 dirToPlayer = transform.position - target.transform.position;

        Vector3 newPos = transform.position + dirToPlayer;
        navMeshAgent.SetDestination(newPos);
    }

    void RunAway()
    {
        Vector3 dirToPlayer = transform.position - target.transform.position;

        Vector3 newPos = transform.position + dirToPlayer;
        navMeshAgent.SetDestination(newPos);
    }

    void RunDown()
    {
        navMeshAgent.SetDestination(target.transform.position);
    }

    void Wander()
    {
        if (timer <= 0)
        {
            timer = 7f;
            navMeshAgent.SetDestination(RandomNavmeshLocation(Mathf.FloorToInt(Random.Range(4, 10))));
        }
    }

    int formula()
    {
        return AttackValue = daCrips.Count * 2 + HitTaken() + PlayerDamaged() + PlayerDistance() + permValue();
    }

    // a value that permenatly affects the attack value, either by a lot or a little
    // right now I just call it and let it do its thing
    // but in the future this wont always call
    // (I have it always call for showcasing purposes, and also I don't know how i'll implement the
    // aspect of not always being called, a bool maybe, looking for a faster way)
    int permValue()
    {
        int permValue = Mathf.FloorToInt(Random.Range(0, 10));
        Debug.Log(permValue);
        return permValue;
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}

// run away code taken from: https://www.youtube.com/watch?v=Zjlg9F3FRJs
