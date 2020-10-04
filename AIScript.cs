﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    [SerializeField]
    Transform player;

    public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
    }
}
