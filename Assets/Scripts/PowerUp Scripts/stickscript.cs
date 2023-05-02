using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickscript : MonoBehaviour
{
    public bool sticky = false;
    public bool sticky2 = false;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ball") && sticky)
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();

            joint.anchor = collision.contacts[0].point;

            joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();

            joint.enableCollision = false;
            sticky = false;

        }        
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
