using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickScript2 : MonoBehaviour
{
    public bool sticky = false;
    public bool sticky2 = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            sticky2 = true;
            if (sticky)
            {
                FixedJoint joint = gameObject.AddComponent<FixedJoint>();

                joint.anchor = collision.contacts[0].point;

                joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();

                joint.enableCollision = false;
                sticky = false;

            }
        }
        else
            sticky2 = false;
    }

    public bool touchingBall()
    {
        return sticky2;

    }
    public void unstick()
    {
        sticky = false;
        sticky2 = false;
        Destroy(gameObject.GetComponent<FixedJoint>());
        Destroy(gameObject.GetComponent<ConfigurableJoint>());
        Destroy(gameObject.GetComponent<CharacterJoint>());
    }

}