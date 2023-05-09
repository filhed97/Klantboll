using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActiveRagdoll
{
    public class ForcedMovement : MonoBehaviour
    {
        //Values supplied by module settings window in editor
        public Rigidbody Joint;
        public Rigidbody RightLegRoot;
        public float MovementSpeed = 7.8f;
        public float WalkSpeedMultiplier = 0.5f;
        public float BoostSpeed = 12f;
        public float HeightDamperOffset = 0.3f;
        public float DamperForceMultiplier = 0.0f;

        public Vector3 Target2D { get; set; }   //not used
        public Vector3 Target3D { get; set; }
        private Vector3 zeroes = new Vector3(0.01f, 0.01f, 0.01f);

        private float initialJointHeight;
        private ConfigurableJoint RLegJoint;
        private JointDrive RLegDrive;

        private bool walking;
        private bool boostMode;
        private float walkModifyer;

        // Start is called before the first frame update
        void Start()
        {
            initialJointHeight = getJointHeight();
            RLegJoint = RightLegRoot.GetComponent<ConfigurableJoint>();
            RLegDrive = RLegJoint.angularXDrive;
            Joint.velocity = zeroes;
            boostMode = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            CheckInputs();
            walkModifyer = 1f;
            if (walking) { walkModifyer = WalkSpeedMultiplier; }
            Vector3 moveDirection = Target3D;
            
            if (Input.GetAxis("Horizontal") == 0 & Input.GetAxis("Vertical") == 0) { Joint.velocity = zeroes; } 
            else 
            {
                if (boostMode)
                {
                    //forced velocity in x,y,z. joint height also set in case player enters boostmode in awkward position.
                    Joint.transform.position = new Vector3(Joint.transform.position.x, initialJointHeight, Joint.transform.position.z);
                    moveDirection.Set(moveDirection.x, 0, moveDirection.z);
                    Joint.velocity = moveDirection * BoostSpeed * walkModifyer; 
                }
                else
                {
                    //y-coordinate is taken from previous frame's velocity, but scaled down.
                    //if not scaled like this, the active ragdoll had a tendency to kick himself to the moon.
                    //only the velocity in x and z coordinates (=forward/backward/left/right from input) is strictly forced.
                    Vector3 newVelocity = new Vector3(moveDirection.x, Joint.velocity.y * 0.1f, moveDirection.z);
                    Joint.velocity = newVelocity * MovementSpeed * walkModifyer;
                }
            }   

            HeightDamper();
        }

        //deals with inputs that affect forced movement
        private void CheckInputs()
        {
            //increases leg power during kick animations to ensure the active ragdoll will 
            //"power through" the animation instead of getting his foot stuck in the ground.
            if (Input.GetKey(KeyCode.Space)) { RLegDrive.positionSpring = 15000; }
            else { RLegDrive.positionSpring = 1500; }

            //changes walking flag if shift is being pressed
            if (Input.GetKey(KeyCode.LeftShift)) { walking = true; }
            else { walking = false; }
        }

        //Pushes player down when he kicks himself too far into the air during sprinting (since active ragdoll is active C:). 
        private void HeightDamper()
        {
            //yVal is the current joint position in world space
            float yVal = getJointHeight();
            //Debug.Log("yVal: "+yVal);

            //if diff is positive, the joint is above its starting position plus offset (additional margin), and triggers a downward force.
            float diff = yVal - initialJointHeight + HeightDamperOffset;
            //float magnitude = kernel_sq(diff);
            float magnitude = kernel_sq(diff);
            if(diff < 0)
            //joint is above threshold
            {
                Joint.AddForce(transform.up * -1 * magnitude);
            }
        }

        //function to increase damper force with distance squared.
        //"kernel" naming convention in case other functions will be added/compared later
        private float kernel_sq(float val)
        {
            //+1 ensures output starts at 1 instead of 0.
            return (1 + val) * (1 + val) * DamperForceMultiplier;
        }

        //constant value, works fine and easier to test with.
        private float kernel_const()
        {
            return 12;
        }

        //returns current joint height in world space
        private float getJointHeight()
        {
            return Joint.transform.position.y;
        }
    }
}
