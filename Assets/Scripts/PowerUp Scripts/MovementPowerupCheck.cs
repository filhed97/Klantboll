using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPowerupCheck : MonoBehaviour
{
    private float movementspeed;
    private float originalSpeed;
    public Animator animator;

    private void Start()
    {
        originalSpeed = GetComponent<ActiveRagdoll.ForcedMovement>().MovementSpeed;
        movementspeed = originalSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        movementspeed = GetComponent<ActiveRagdoll.ForcedMovement>().MovementSpeed;
        if(movementspeed == 0)
        {
            animator.SetBool("iceDebuff", true);
        }
        else if(movementspeed > originalSpeed)
        {
        }
        else if(movementspeed < originalSpeed)
        {
            
            animator.SetBool("slowDebuff", true);
            Debug.Log(animator.GetBool("slowDebuff"));
        }
        else
        {
            animator.SetBool("iceDebuff", false);
            animator.SetBool("slowDebuff", false);
        }


    }
}
