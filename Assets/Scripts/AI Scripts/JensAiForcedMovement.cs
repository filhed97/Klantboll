using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActiveRagdoll
{
    public class JensAiForcedMovement : MonoBehaviour
    {
        // Values supplied by module settings window in editor
        public Rigidbody Joint;
        public Rigidbody RightLegRoot;
        private float MovementSpeed = 4.5F;
        public float HeightDamperOffset = 0.1f;
        public float DamperForceMultiplier = 0.0f;
        public float BoostSpeedMultiplier = 1.5f;
        public GameObject ragdollOnKickBodyPart;
        public float DefaultMovementSpeed = 7.8f; 
        public bool boostMode;
        public Vector2 Target2D { get; set; }       //currently unused
        public Vector3 Target3D { get; set; }

        private Vector3 zeroes = new Vector3(0.01f, 0.01f, 0.01f);
        private float initialJointHeight;
        private ConfigurableJoint RLegJoint;
        private JointDrive RLegDrive;

        // AI Variables
        public Transform target; // Assign the ball target in the Inspector
        public bool targetIsBall;
        public Transform goalTarget; // Assign the goal target in the Inspector
        private float kickDistance = 1.5f; // Distance to the ball to attempt a kick
        public float kickForce = 300f; // Force applied to the ball when kicked
        public Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            initialJointHeight = getJointHeight();
            RLegJoint = RightLegRoot.GetComponent<ConfigurableJoint>();
            RLegDrive = RLegJoint.angularXDrive;
            Joint.velocity = zeroes;
            boostMode = false;
        }

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

        public float GetSpeed() { return MovementSpeed; }
        // Update is called once per frame
        void FixedUpdate()
        {
            // AI decision-making logic
            Vector3 targetDirection = target.position - Joint.position;
            targetDirection.y = 0; // We don't want to move vertically
            targetDirection.Normalize();

            // Control the AI movement based on the targetDirection
            //Vector3 newVelocity = new Vector3(targetDirection.x, 0, targetDirection.z);

            float stoppingDistance = 1.0f; // Add stopping distance to prevent getting too close
            if (Vector3.Distance(target.position, Joint.position) > stoppingDistance & !ragdollOnKickBodyPart.GetComponent<JanneRagdollOnKick>().getRagdolled())
            {
                //  Joint.velocity = newVelocity * MovementSpeed;
                if (boostMode)
                {
                    //forced velocity in x,y,z. joint height also set in case player enters boostmode in awkward position.
                    Joint.transform.position = new Vector3(Joint.transform.position.x, initialJointHeight, Joint.transform.position.z);
                    Vector3 newVelocity = new Vector3(targetDirection.x, 0, targetDirection.z);
                    Joint.velocity = newVelocity * MovementSpeed;
                }

                else
                {
                Vector3 newVelocity = new Vector3(targetDirection.x, Joint.velocity.y * 0.1f, targetDirection.z);
                Joint.velocity = newVelocity * MovementSpeed;
            }
            else
            {
                if (!ragdollOnKickBodyPart.GetComponent<JanneRagdollOnKick>().getRagdolled())
                    Joint.velocity = Vector3.zero; // Stop the AI if it's within the stopping distance
            }

            // Kick the ball if close enough
            if (targetIsBall & Vector3.Distance(target.position, Joint.position) < kickDistance & !ragdollOnKickBodyPart.GetComponent<JanneRagdollOnKick>().getRagdolled())
            {
                animator.SetBool("kick", true);
                Rigidbody ballRigidbody = target.GetComponent<Rigidbody>();
                Vector3 kickDirection = (goalTarget.position - target.position).normalized;
                ballRigidbody.AddForce(kickDirection * kickForce);

            }
            else if (!targetIsBall & Vector3.Distance(target.position, Joint.position) < kickDistance & !ragdollOnKickBodyPart.GetComponent<JanneRagdollOnKick>().getRagdolled())
            {
                animator.SetBool("kick", true);
            }

            HeightDamper();
        }


        private void HeightDamper()
        {
            float yVal = getJointHeight();
            float diff = yVal - initialJointHeight + HeightDamperOffset;
            float magnitude = kernel_sq(diff);
            if (diff < 0)
            //joint is above threshold
            {
                Joint.AddForce(transform.up * -1 * -magnitude);
            }
        }

        private float kernel_sq(float val)
        {
            //+1 ensures output starts at 1 instead of 0.
            return (1 + val) * (1 + val) * DamperForceMultiplier;
        }

        private float getJointHeight()
        {
            return Joint.transform.position.y;
        }
    }
}
