using System;
using UnityEditor;
using UnityEngine;

namespace ActiveRagdoll {
    public class AIAnimationModule : Module {
        [Header("--- BODY ---")]
        private Quaternion[] _initialJointsRotation;
        private ConfigurableJoint[] _joints;
        private Transform[] _animatedBones;
        private AnimatorHelper _animatorHelper;
        public Animator Animator { get; private set; }

        private AIForcedMovement aiForcedMovement;
        public GameObject ragdollOnKickBodyPart;
        // New properties for movement and kicking animation
        public Vector3 MovementDirection { get; set; }
        public bool IsKicking { get; set; }

        // New method to update the movement direction of the animated model
private void UpdateMovementDirection()
{
    if (MovementDirection == Vector3.zero) return;

    float rotationSpeed = 20f; // Adjust this value to control the rotation speed

    if(ragdollOnKickBodyPart.GetComponent<JanneRagdollOnKick>().getRagdolled())
        rotationSpeed = 0;

    Quaternion targetRotation = Quaternion.LookRotation(MovementDirection);
    _activeRagdoll.transform.rotation = Quaternion.RotateTowards(_activeRagdoll.transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
}




        private void Start() {
            _joints = _activeRagdoll.Joints;
            _animatedBones = _activeRagdoll.AnimatedBones;
            _animatorHelper = _activeRagdoll.AnimatorHelper;
            Animator = _activeRagdoll.AnimatedAnimator;

            _initialJointsRotation = new Quaternion[_joints.Length];
            for (int i = 0; i < _joints.Length; i++) {
                _initialJointsRotation[i] = _joints[i].transform.localRotation;
            }

            // Get the AIForcedMovement component
            aiForcedMovement = GetComponent<AIForcedMovement>();
        }

        void FixedUpdate() {
            UpdateJointTargets();
            UpdateAnimatorParameters(); // Update Animator parameters each FixedUpdate
            UpdateMovementDirection(); // Add this line to update the AI model's rotation
        }


        private void UpdateJointTargets() {
            for (int i = 0; i < _joints.Length; i++) {
                ConfigurableJointExtensions.SetTargetRotationLocal(_joints[i], _animatedBones[i + 1].localRotation, _initialJointsRotation[i]);
            }
        }

        // New method to update the Animator parameters based on movement and kicking
        private void UpdateAnimatorParameters() {
            // Update the "Speed" parameter based on the magnitude of the MovementDirection
            Animator.SetFloat("Speed", MovementDirection.magnitude);

            // Update the "IsKicking" parameter based on the IsKicking property
            Animator.SetBool("IsKicking", IsKicking);
        }
    }
}
