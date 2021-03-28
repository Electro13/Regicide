using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillGod
{
    public class ResetAnimatorBool : StateMachineBehaviour
    {
        public string targetBool;
        public string targetBool2;
        public bool status;
        public bool status2;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(targetBool, status);
            animator.SetBool(targetBool2, status2);
        }
    }
}
