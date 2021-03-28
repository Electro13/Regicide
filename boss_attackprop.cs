using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles attack properties, but mainly on the positioning and
// effects side of things
// if possible the activation of hitboxes and damage control will happen here as well

public class boss_attackprop : StateMachineBehaviour
{
    BossStorage bossStorage;
    public bool adjustPos = true;
    Transform transform;

    // Some attacks shouldn't have left hand IK, by default this is disabled
    public float attack_weight = 0f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossStorage = GameObject.FindGameObjectWithTag("bossController").GetComponent<BossStorage>();
        transform = animator.GetComponent<Transform>();
        bossStorage.setTwoBoneIK(attack_weight);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // use this if the attack causes the  model to move away from the hitbox
        if(adjustPos)
        {
            bossStorage.gameObject.transform.position = transform.position;
            bossStorage.gameObject.transform.rotation = transform.rotation;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
