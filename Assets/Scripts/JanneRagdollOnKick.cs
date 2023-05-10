using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JanneRagdollOnKick : MonoBehaviour
{
    [Tooltip("Add the highest parent, In the case of Janne: \"Player\"")]
    public GameObject rigToGetJointsFrom;
    
    private ConfigurableJoint[] joints;
    private Rigidbody[] bodyparts;

    [Tooltip("Write the name of the boolean value for the boolean kick parameter in the animator for the opponent")]
    public string KickParameterName;

    [Tooltip("Add the gameObject names which should be used for collision when being kicked for example: Leg.L,Shin.R and so on.")]
    public string[] collisions;

    [Tooltip("Add the name of the animated version of the character. The root name. In the case of Janne: \"Animated\"")]
    public string animatedVersionName;

    public bool ragdolled = false;
    [Tooltip("The amount of force eachbody part will be hit with when the ragdoll is kicked by another player")]
    public float kickedForce = 100.0f;

    private float startTime;
    public float ragdollTime = 5.0f;

    private List<float> driveXOriginal;
    private List<float> driveYZOriginal;
    private List<float> damperXOriginal;
    private List<float> damperYZOriginal;

    //Maybe use some other time
    /* [Tooltip("The angularXDrive SpringPosition power")]
     public float springPowerX = 400.0f;

     [Tooltip("The angularYZDrive SpringPosition power")]
     public float springPowerYZ = 400.0f;
    */
    void Start()
    {
        StartCoroutine(WaitForStabalizer());
      
    }


    IEnumerator WaitForStabalizer()
    {
        yield return new WaitForSeconds(0.05F);

        driveXOriginal = new List<float>();
        driveYZOriginal = new List<float>();
        damperXOriginal = new List<float>();
        damperYZOriginal = new List<float>();
    joints = rigToGetJointsFrom.GetComponentsInChildren<ConfigurableJoint>();

        bodyparts = rigToGetJointsFrom.GetComponentsInChildren<Rigidbody>();
        foreach (ConfigurableJoint j in joints)
        {
            JointDrive drive = j.angularXDrive;
            driveXOriginal.Add(drive.positionSpring);
            damperXOriginal.Add(drive.positionDamper);

            drive = j.angularYZDrive;
            driveYZOriginal.Add(drive.positionSpring);
            damperYZOriginal.Add(drive.positionDamper);

            //Debug.Log(j.name);

        }
    }

    IEnumerator WaitForRagdoll()
    {
        yield return new WaitForSeconds(0.05F);

        ragdollOn();
    }

    void ragdollOn()
    {
        ragdolled = true;
        //Debug.Log("Ragdoll supposed to turn on");
        foreach (ConfigurableJoint j in joints)
        {
            JointDrive jointDrive = j.angularXDrive;
            jointDrive.positionSpring = 1.0f;
            jointDrive.positionDamper = 0;
            j.angularXDrive = jointDrive;
         

            jointDrive = j.angularYZDrive;
            jointDrive.positionSpring = 1.0f;
            jointDrive.positionDamper = 0;
         
            j.angularYZDrive = jointDrive;
        }
       // Debug.Log("Ragdolled turned on");

    }


    void ragdollOff()
    {
        ragdolled = false;

        int i = 0;
        foreach (ConfigurableJoint j in joints)
        {
            JointDrive jointDrive = j.angularXDrive;
            jointDrive.positionSpring = driveXOriginal[i];
            jointDrive.positionDamper = damperXOriginal[i];

            j.angularXDrive = jointDrive;

            jointDrive = j.angularYZDrive;
            jointDrive.positionSpring = driveYZOriginal[i];
            jointDrive.positionDamper = damperYZOriginal[i];
           

            j.angularYZDrive = jointDrive;
            i++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject CollidedWith = collision.gameObject;
        if (!CollidedWith.transform.IsChildOf(rigToGetJointsFrom.transform))
        {
            
            if (collisions.Contains(collision.gameObject.name))
            {
               // Debug.Log("Collided with: " + collision.gameObject.name);
                if(CollidedWith.transform.root.Find(animatedVersionName).GetComponent<Animator>().GetBool(KickParameterName) == true)
                {
                    //Debug.Log("Kicked by: " + CollidedWith.name);
                    ragdollOn();
                    startTime = Time.time;
                   
                    //Maybe use if transform forward sucks
                    /*
                    Vector3 direction;
                    direction = transform.position - CollidedWith.transform.position;
                    direction = direction.normalized;
                    */
                    
                    foreach (Rigidbody rb in bodyparts)
                    {
                        rb.AddForce(CollidedWith.transform.forward.normalized*kickedForce,ForceMode.Impulse);  
                    }
                  Debug.Log("Force added!");
                }     
            }
            
        }
    }

    public bool getRagdolled()
    {
        return ragdolled;
    }

    private void Update()
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
