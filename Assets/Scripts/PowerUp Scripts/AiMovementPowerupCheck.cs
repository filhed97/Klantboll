using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovementPowerupCheck : MonoBehaviour
{
    private float originalSpeed;
    public Animator animator;

    private void Start()
    {
        originalSpeed = GetComponent<ActiveRagdoll.JensAiForcedMovement>().DefaultMovementSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        float movementspeed = GetComponent<ActiveRagdoll.JensAiForcedMovement>().GetSpeed();
        if(movementspeed < 0.1f)
        {
            animator.SetBool("iceDebuff", true);
        }
        else if(movementspeed > originalSpeed*1.1f)
        {
            animator.SetBool("boost", true);
        }
        else if(movementspeed < originalSpeed*0.9f)
        {
            animator.SetBool("slowDebuff", true);
        }
        else
        {
            animator.SetBool("iceDebuff", false);
            animator.SetBool("slowDebuff", false);
            animator.SetBool("boost", false);
        }


    }
}
