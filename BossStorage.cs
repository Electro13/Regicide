using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

// This script handles things that always need to occur, or outside of certain animation windows
public class BossStorage : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public float BossSide_Z;
    public float BossSide_X;

    public Vector3 bossOffset;
    public Animator bossAnim;
    TwoBoneIKConstraint TwoBoneIK;
    public float twoboneIK;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        bossAnim = GameObject.FindGameObjectWithTag("bossModel").GetComponent<Animator>();
        TwoBoneIK = GameObject.Find("LeftHandIK").GetComponent<TwoBoneIKConstraint>();
        setTwoBoneIK(0f);
    }

    // Update is called once per frame
    void Update()
    {
        TwoBoneIK.weight = twoboneIK;

        Debug.Log(TwoBoneIK.weight);
        if(!player) player = GameObject.FindGameObjectWithTag("Player").transform;

        bossAnim.SetFloat("playerDirectionZ", BossSide_Z);
        bossAnim.SetFloat("playerDirectionX", BossSide_X);

        // using the dot product we can determine which side the player is on relation to the boss
        // if it returns 1 that means we're directly infront of it, likewise -1 means the opposite
        // BossSide_X is the same but with left and right
        bossOffset = (player.position - transform.position).normalized;
        BossSide_Z = Vector3.Dot(bossOffset, transform.forward.normalized);
        BossSide_X = Vector3.Dot(bossOffset, transform.right.normalized);
        //Debug.Log(BossSide_Z);
    }

    // this is like Mathf.Approximately but with a threshold
    public bool FastApproximately(float a, float b, float threshold)
    {
        return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= threshold;
    }

    // A function to allow to adjust rotation easily
    public void rotateDeg(float turnAnglex, float turnAngley, float turnAnglez)
    {
        transform.Rotate(turnAnglex, turnAngley, turnAnglez);
    }

    // Yes, it has to work like this
    // I do not know why either
    public void setTwoBoneIK(float weight)
    {
        //TwoBoneIK.weight = weight;
        twoboneIK = weight;
        Debug.Log("changed to " + weight);
    }
}
