using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActiveRagdoll
{
    public class MPForcedMovement : MonoBehaviour
    {
        //Values supplied by module settings window in editor
        public Rigidbody Joint;
        public Rigidbody RightLegRoot;
        public float MovementSpeed = 7f;
        public float HeightDamperOffset = 0.1f;
        public float DamperForceMultiplier = 0.0f;

        public Vector2 Target2D { get; set; }       //currently unused
        public Vector3 Target3D { get; set; }

        private Vector3 zeroes = new Vector3(0.01f, 0.01f, 0.01f);
        private float initialJointHeight;
        private ConfigurableJoint RLegJoint;
        private JointDrive RLegDrive;

        // Start is called before the first frame update
        void Start()
        {
            initialJointHeight = getJointHeight();
            RLegJoint = RightLegRoot.GetComponent<ConfigurableJoint>();
            RLegDrive = RLegJoint.angularXDrive;
            Joint.velocity = zeroes;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 moveDirection = Target3D;
            //moveDirection.Set(moveDirection.x, 0, moveDirection.z);
            //Vector3 moveDirection = smoothedInput(Joint.velocity, Target3D);

            //changes velocity of RigidBody rb
            //Debug.Log("Target2D: " + Target2D);
            if (Input.GetAxis("Horizontal") == 0 & Input.GetAxis("Vertical") == 0) {
                //joint.transform.forward = moveDirection;
                Joint.velocity = zeroes;
                //Joint.velocity = smoothedInput(Joint.velocity, zeroes);
            } else {
                //changes velocity of RigidBody rb
                //Joint.velocity = moveDirection * movementInertiaModifier(MovementSpeed);

                //Joint.velocity = smoothedInput(Joint.velocity, moveDirection);

                Vector3 newVelocity = new Vector3(moveDirection.x, Joint.velocity.y*0.1f, moveDirection.z);
                Joint.velocity = newVelocity * MovementSpeed;

                //Debug.Log("vel = " + Joint.velocity);
            }

            if (Input.GetButton("space"))
            {      
                RLegDrive.positionSpring = 15000;
            }
            else
            {
                RLegDrive.positionSpring = 1500;
            }

            //Debug.Log("xdrive: " + RLegDrive.positionSpring);

            HeightDamper();
        }

        private void HeightDamper()
        {
            float yVal = getJointHeight();
            //Debug.Log("yVal: "+yVal);
            float diff = yVal - initialJointHeight + HeightDamperOffset;
            //float magnitude = kernel_sq(diff);
            float magnitude = -12;
            if(diff < 0)
            //joint is above threshold
            {
                Joint.AddForce(transform.up * -magnitude);
            }
            //joint is below threshold
            else
            {
                //unsure if needed
                //Joint.AddForce(transform.up * magnitude);
            }
        }

        private float kernel_sq(float val)
        {
            return (1 + val) * (1 + val) * DamperForceMultiplier;
        }

        private float getJointHeight()
        {
            return Joint.transform.position.y;
        }

        private float movementInertiaModifier(float val)
        {
            float mag = Joint.velocity.magnitude;
            //if (mag < 0.01) { return val; }
            //return mag * val;
            return (MovementSpeed + mag) / 10;
        }

        private Vector3 smoothedInput(Vector3 currentVelocity, Vector3 inputVector)
        {
            float magnitude = MovementSpeed*(0.5f/(0.5f+(currentVelocity.normalized - inputVector.normalized).magnitude));
            Debug.Log("Mag: " + magnitude);
            return Vector3.Normalize((currentVelocity.normalized+inputVector.normalized)) * magnitude;
        }
    }
}
