using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    
    public float jumpHeight = 15;
    public float MovementSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //directional vector for movement and character rotation.
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

        //changes velocity of RigidBody rb
        rb.velocity += moveDirection*MovementSpeed;

        //if statement that handles jump
        if(Input.GetKeyDown("space") && IsGrounded()){
            rb.velocity += new Vector3(Input.GetAxis("Horizontal"),jumpHeight,Input.GetAxis("Vertical"));
        }
    }

    bool IsGrounded(){
        return Physics.CheckSphere(groundCheck.position, 0.1f, ground);
    }
}
