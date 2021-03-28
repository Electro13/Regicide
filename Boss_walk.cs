using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This script is called the most, as it controls just the movement and AI of the boss

public class Boss_walk : StateMachineBehaviour
{
    BossStorage bossStorage;
    Transform transform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossStorage = GameObject.FindGameObjectWithTag("bossController").GetComponent<BossStorage>();

        // We disable root motion for movement animations because it's easier to manage 
        // (and we can use less animations)
        animator.applyRootMotion = false;
        transform = animator.GetComponent<Transform>();

        if (bossStorage.agent.isStopped)
            bossStorage.agent.isStopped = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Since the actual boss hitbox and model are different we have to set the position ourselves
        #region but why?
        /*
         * The reason why we do it like this is because of root motion and simplicity
         * Since root motion only affects the models (and whatever child it has) xyz and rotation
         * it would not be possible match up the hitbox of the boss without removing the child-parent relationship
         * for peace of mind it's easier to just have them seperate from the get go
         * don't worry, it's functionally the same. 
         * The only downfall is the fact we'd have to handle rotation ourselves and make sure positions and rotations
         * match up properly, and its more prone to looking.. weird under certain situations, though those situations are less fault of logic
         * and more outright bugs.
         * 
         */

        #endregion

        transform.position = bossStorage.gameObject.transform.position;
        transform.rotation = bossStorage.gameObject.transform.rotation;

        if(Vector3.Distance(bossStorage.player.transform.position, transform.position) < 5f)
        {
            animator.SetTrigger("Attack");
        }

        if (bossStorage.FastApproximately(animator.GetFloat("playerDirectionZ"), -1.0f, 0.2f))
            animator.SetBool("playerbehind", true);
        else animator.SetBool("playerbehind", false);

        bossStorage.agent.SetDestination(bossStorage.player.transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossStorage.agent.isStopped = true;
        animator.applyRootMotion = true;
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
