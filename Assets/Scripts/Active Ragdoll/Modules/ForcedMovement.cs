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
        public float DefaultMovementSpeed = 7.8f;
        private float MovementSpeed;
        public float WalkSpeedMultiplier = 0.5f;
        public float BoostSpeedMultiplier = 1.5f;
        public float HeightDamperOffset = 0.3f;
        public float DamperForceMultiplier = 2f;

        public Vector3 Target2D { get; set; }   //not used
        public Vector3 Target3D { get; set; }
        private Vector3 zeroes = new Vector3(0.01f, 0.01f, 0.01f);

        private float initialJointHeight;
        private ConfigurableJoint RLegJoint;
        private JointDrive RLegDrive;
 
        public bool boostMode;
        private bool walking;
        private float walkModifyer;

        //Ensures MovementSpeed can only be read, and not set explicitly by external scripts.
        //If MovementSpeed needs to be changed, use MultiplySpeedByFactor();
        public float GetSpeed() { return MovementSpeed; }

        //This method ensures MovementSpeed can only be changed externally by multiplication.
        //If a powerup script needs to increase/decrease MovementSpeed, it will be forced to apply this
        //change of value by multiplying it by a factor x, then once finished, revert the change
        //by multiplying MovementSpeed by the inverse factor (1/x).
        //This ensures any simultaneous modifyers to MovementSpeed work together without issue.
        //Note; because the factor needs to be inverted when reverting your change,
        //the factor cannot be 0! Use a low value like 0.01 instead.
        public void MultiplySpeedByFactor(float factor)
        {
            if (factor == 0f)
            {
                Debug.Log("MovementSpeed cannot be multiplied by 0! MovementSpeed has been left unchanged.");
            }
            else
            {
                MovementSpeed *= factor;
                Debug.Log("MovementSpeed, factor: " + MovementSpeed + ", " + factor);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            MovementSpeed = DefaultMovementSpeed;
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
                    Vector3 newVelocity = new Vector3(moveDirection.x, 0, moveDirection.z);
                    Joint.velocity = newVelocity * MovementSpeed * walkModifyer; 
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

            //if diff is positive, the joint is above its starting position plus offset (additional margin), and triggers a downward force.
            float diff = yVal - initialJointHeight + HeightDamperOffset;
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
