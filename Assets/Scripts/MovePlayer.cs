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

        float speed = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        transform.Rotate(0, speed, 0);
        rb.AddForce(transform.forward * 50);
    }

    bool IsGrounded(){
        return Physics.CheckSphere(groundCheck.position, 0.1f, ground);
    }
}
