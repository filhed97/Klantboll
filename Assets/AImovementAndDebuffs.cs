using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImovementAndDebuffs : StateMachineBehaviour
{

    public float animationOgSpeed = 0.6F;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        if (animator.GetBool("iceDebuff"))
        {
            animator.speed = 0F;
            animator.transform.root.GetComponent<AIDefaultBehaviour>().enabled = false;
        }

        else if (animator.GetBool("slowDebuff"))
        {
            animator.speed = 0.45F;
        }

        else if (animator.GetBool("boost"))
        {
            animator.speed = 1.5F;
        }

        else
        {
            animator.speed = animationOgSpeed;
            animator.transform.root.GetComponent<AIDefaultBehaviour>().enabled = true;
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
