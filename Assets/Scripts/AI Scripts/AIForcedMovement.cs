using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActiveRagdoll
{
    public class AIForcedMovement : MonoBehaviour
    {
        // Values supplied by module settings window in editor
        public Rigidbody Joint;
        public Rigidbody RightLegRoot;
        public float MovementSpeed = 2.0f;
        public float HeightDamperOffset = 0.1f;
        public float DamperForceMultiplier = 0.0f;

        public Vector2 Target2D { get; set; }       //currently unused
        public Vector3 Target3D { get; set; }

        private Vector3 zeroes = new Vector3(0.01f, 0.01f, 0.01f);
        private float initialJointHeight;
        private ConfigurableJoint RLegJoint;
        private JointDrive RLegDrive;

        // AI Variables
        public Transform target; // Assign the ball target in the Inspector
        public Transform goalTarget; // Assign the goal target in the Inspector
        private float kickDistance = 1.5f; // Distance to the ball to attempt a kick
        public float kickForce = 300f; // Force applied to the ball when kicked

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
    // AI decision-making logic
    Vector3 targetDirection = target.position - Joint.position;
    targetDirection.y = 0; // We don't want to move vertically
    targetDirection.Normalize();

    // Control the AI movement based on the targetDirection
    Vector3 newVelocity = new Vector3(targetDirection.x, 0, targetDirection.z);

    float stoppingDistance = 1.0f; // Add stopping distance to prevent getting too close
    if (Vector3.Distance(target.position, Joint.position) > stoppingDistance)
    {
        Joint.velocity = newVelocity * MovementSpeed;
    }
    else
    {
        Joint.velocity = Vector3.zero; // Stop the AI if it's within the stopping distance
    }

    // Kick the ball if close enough
    if (Vector3.Distance(target.position, Joint.position) < kickDistance)
    {
        Rigidbody ballRigidbody = target.GetComponent<Rigidbody>();
        Vector3 kickDirection = (goalTarget.position - target.position).normalized;
        ballRigidbody.AddForce(kickDirection * kickForce);
    }

    HeightDamper();
}


        private void HeightDamper()
        {
            float yVal = getJointHeight();
            float diff = yVal - initialJointHeight + HeightDamperOffset;
            float magnitude = -12;
            if (diff < 0)
            //joint is above threshold
            {
                Joint.AddForce(transform.up * -magnitude);
            }
        }

        private float getJointHeight()
        {
            return Joint.transform.position.y;
        }
    }
}
