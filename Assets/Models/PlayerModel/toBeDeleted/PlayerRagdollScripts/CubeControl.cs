using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour
{
    public Rigidbody cube;
    public float MovementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

        //changes velocity of RigidBody rb
          if(moveDirection != Vector3.zero){
            cube.transform.forward = moveDirection;
        }

        //changes velocity of RigidBody rb
        cube.velocity = moveDirection*MovementSpeed;
    }
}
