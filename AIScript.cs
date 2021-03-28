using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    [SerializeField]
    Transform player;

    public NavMeshAgent agent;
    public float timer;

    [SerializeField]
    bool Follow;

    void Start()
    {
        timer = 7f;   
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 7f;
            if(Random.Range(0,2) == 0)
            {
                Follow = false;
                agent.SetDestination(RandomNavmeshLocation(Random.Range(0, 10)));
            }
            else Follow = true;

        }
        if(Follow) agent.SetDestination(player.position);

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
