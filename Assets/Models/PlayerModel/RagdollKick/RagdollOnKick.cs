using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnKick : MonoBehaviour
{
    public GameObject rigToGetJointsFrom;
    ConfigurableJoint[] joints;
    Rigidbody[] bodyparts;
    public ParticleSystem explosion;


    private bool ragdolled = false;
    public float kickForce = 100.0f;

    private float startTime;
    public float ragdollTime = 5.0f;
    void Start()
    {
        joints = rigToGetJointsFrom.GetComponentsInChildren<ConfigurableJoint>();
        bodyparts = rigToGetJointsFrom.GetComponentsInChildren<Rigidbody>();
    }


    void ragdollOn()
    {
        ragdolled = true;
        transform.root.Find("PlayerHolderCube").GetComponent<CubeControl>().enabled = false;
        foreach (ConfigurableJoint j in joints)
        {
            JointDrive jointDrive = j.angularXDrive;
            jointDrive.positionSpring = 1.0f;
            j.angularXDrive = jointDrive;

            jointDrive = j.angularYZDrive;
            jointDrive.positionSpring = 1.0f;
            j.angularYZDrive = jointDrive;

        }


        ConfigurableJoint root = rigToGetJointsFrom.GetComponent<ConfigurableJoint>();
        JointDrive jointDrive2 = root.angularXDrive;
        jointDrive2.positionSpring = 1.0f;
        root.angularXDrive = jointDrive2;

        jointDrive2 = root.angularYZDrive;
        jointDrive2.positionSpring = 1.0f;
        root.angularYZDrive = jointDrive2;

        
    }


    void ragdollOff()
    {
        ragdolled = false;
        transform.root.Find("PlayerHolderCube").GetComponent<CubeControl>().enabled = true;
        foreach (ConfigurableJoint j in joints)
        {
            JointDrive jointDrive = j.angularXDrive;
            jointDrive.positionSpring = 10000.0f;
            j.angularXDrive = jointDrive;

            jointDrive = j.angularYZDrive;
            jointDrive.positionSpring = 10000.0f;
            j.angularYZDrive = jointDrive;
        }

        ConfigurableJoint root = rigToGetJointsFrom.GetComponent<ConfigurableJoint>();
        JointDrive jointDrive2 = root.angularXDrive;
        jointDrive2.positionSpring = 10000.0f;
        root.angularXDrive = jointDrive2;

        jointDrive2 = root.angularYZDrive;
        jointDrive2.positionSpring = 10000.0f;
        root.angularYZDrive = jointDrive2;
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject CollidedWith = collision.gameObject;
        if (!CollidedWith.transform.IsChildOf(rigToGetJointsFrom.transform))
        {
            
            if(CollidedWith.name == "KickCollision")
            {
                if(CollidedWith.transform.root.GetComponent<AnimationControl>().isKicking == true)
                {
                    transform.root.GetComponent<AudioSource>().Play();
                    ragdollOn();
                    startTime = Time.time;
                    explosion.Play();
                    Vector3 direction;
                    direction = transform.position - CollidedWith.transform.position;
                    direction = direction.normalized;
                    foreach (Rigidbody rb in bodyparts)
                    {
                        rb.AddForce(direction*kickForce);
                    }
                }     
            }
            
        }
    }

    private void FixedUpdate()
    {
        if (ragdolled == true)
        {
            if((Time.time - startTime) >= ragdollTime)
            {
                ragdollOff();
            }
        }
    }
}
