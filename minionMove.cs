using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class minionMove : StateMachineBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    public string objectName;

    ControlPanel ControlPanel;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        ControlPanel = animator.GetComponent<ControlPanel>();
        animator.SetFloat("Vertical", 0);
        objectName = animator.name;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float vert = Mathf.Clamp(Vector3.Distance(player.transform.position, animator.transform.position), 1f, 4f);
        float rawDistToPlayer = Vector3.Distance(player.transform.position, animator.transform.position);

        if (rawDistToPlayer < 10f)
        {
            animator.SetFloat("Vertical", vert);
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            animator.SetFloat("Vertical", 0);
            agent.isStopped = true;
        }

        if (rawDistToPlayer <= 3) agent.speed = 7;
        else agent.speed = 3.5f;

        if (rawDistToPlayer <= 2f)
        {
            agent.enabled = false;
            animator.CrossFade("Longs_Attack_p_R", 0.2f);
            ControlPanel.StartCoroutine(ControlPanel.DelayCode(2f, () =>
            {
                animator.CrossFade("Locomotion", 0.2f);
                agent.enabled = true;

            }));
        }

        //Debug.Log(rawDistToPlayer);


        //Debug.Log(Mathf.Clamp(Vector3.Distance(player.transform.position, animator.transform.position), 1f, 4f));
    }

    //code for non player stuff
    void PlayerCheck(Action code)
    {
        if(objectName != player.name)
        {
            code();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
