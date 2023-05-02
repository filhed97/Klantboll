using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public GameObject AnimationPlayer;
    public bool isKicking = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Mkey") && isKicking == false)
        {

            AnimationPlayer.GetComponent<Animator>().Play("Kick");
            isKicking = true;
        }

        else if((Input.GetButton("Vertical") || Input.GetButton("Horizontal")) && isKicking == false)
        {
            AnimationPlayer.GetComponent<Animator>().Play("Run");
        }

        else if(!Input.GetButton("Vertical") && !Input.GetButton("Horizontal") && isKicking == false)
        {
            AnimationPlayer.GetComponent<Animator>().Play("Idle");
        }

    }
}
