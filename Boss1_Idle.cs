using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;


// This script handles what the boss does during it's idle animation
// most bosses should return to this phase after an action like attacking
public class Boss1_Idle : StateMachineBehaviour
{
    BossStorage bossStorage;
    Transform transform;

    // this allows to easily reference the animator string
    public static readonly int hashMoveToPlayer = Animator.StringToHash("MoveToPlayer");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossStorage = GameObject.FindGameObjectWithTag("bossController").GetComponent<BossStorage>();
        transform = animator.GetComponent<Transform>();
        bossStorage.setTwoBoneIK(1f);
        animator.ResetTrigger("Attack");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(bossStorage.player.transform.position, transform.position) < 20f)
        {
            // Truthfully I don't even know if this is needed, but I feel this is a good spot for a failsafe should the hitbox
            // ever be disjointed
            if (bossStorage.gameObject.transform.position != transform.position)
                bossStorage.gameObject.transform.position = transform.position;
            if (bossStorage.gameObject.transform.rotation != transform.rotation)
                bossStorage.gameObject.transform.rotation = transform.rotation;

            if (bossStorage.player)
                animator.SetBool(hashMoveToPlayer, true);
            else
                animator.SetBool(hashMoveToPlayer, false);
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
