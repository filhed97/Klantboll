using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawn : MonoBehaviour
{
    Rigidbody rb;
    public stickscript stick;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.position = new Vector3(0,1,0);
    }

    private void OnTriggerEnter(Collider other) {
        //if(other.gameObject.tag.Equals("Goal") && TieBreakerScript.endOfMatch == 0) {
        //    Debug.Log("BallRespawn");
        //    stick.unstick();
        //    rb.Sleep();
        //    rb.position = new Vector3(0,1,0);
        //    rb.velocity = Vector3.zero;
        //    rb.WakeUp();
        //}
        //Debug.Log("OnTriggerEvent");
    }

    private void Update()
    {
        if (GoalRegister.toRespawnBall == 1)
        {
            respawnBall();
        }
    }

    public void respawnBall()
    {
        stick.unstick();
        rb.Sleep();
        rb.position = new Vector3(0, 1, 0);
        rb.velocity = Vector3.zero;
        rb.WakeUp();
    }
}
