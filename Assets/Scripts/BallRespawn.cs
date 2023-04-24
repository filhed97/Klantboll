using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawn : MonoBehaviour
{
    Rigidbody rb;
    public Transform SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.position = new Vector3(0,1,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals("Goal")) {
            rb.Sleep();
            rb.position = new Vector3(0,1,0);
            rb.velocity = Vector3.zero;
            rb.WakeUp();
        }
    }
}
