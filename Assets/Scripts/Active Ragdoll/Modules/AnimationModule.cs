using System;
using UnityEditor;
using UnityEngine;

namespace ActiveRagdoll {
    // Author: Sergio Abreu García | https://sergioabreu.me

    public class AnimationModule : Module {
        [Header("--- BODY ---")]
        /// <summary> Required to set the target rotations of the joints </summary>
        private Quaternion[] _initialJointsRotation;
        private ConfigurableJoint[] _joints;
        private Transform[] _animatedBones;
        private AnimatorHelper _animatorHelper;
        public Animator Animator { get; private set; }
        
        public Vector3 AimDirection { get; set; }
        private Vector3 _armsDir, _lookDir, _targetDir2D;
        private Transform _animTorso, _chest;
        private float _targetDirVerticalPercent;

        private void Start() {
            _joints = _activeRagdoll.Joints;
            _animatedBones = _activeRagdoll.AnimatedBones;
            _animatorHelper = _activeRagdoll.AnimatorHelper;
            Animator = _activeRagdoll.AnimatedAnimator;

            _initialJointsRotation = new Quaternion[_joints.Length];
            for (int i = 0; i < _joints.Length; i++) {
                _initialJointsRotation[i] = _joints[i].transform.localRotation;
            }
        }

        void FixedUpdate() {
            UpdateJointTargets();
        }

        /// <summary> Makes the physical bones match the rotation of the animated ones </summary>
        private void UpdateJointTargets() {
            for (int i = 0; i < _joints.Length; i++) {
                ConfigurableJointExtensions.SetTargetRotationLocal(_joints[i], _animatedBones[i + 1].localRotation, _initialJointsRotation[i]);
            }
        }
        
        /// <summary> Reflect the direction when looking backwards, avoids neck-breaking twists </summary>
        /// <param name=""></param>
        private void ReflectBackwards() {
            bool lookingBackwards = Vector3.Angle(AimDirection, _animTorso.forward) > 90;
            if (lookingBackwards) AimDirection = Vector3.Reflect(AimDirection, _animTorso.forward);
        }

        /// <summary> Plays an animation using the animator. The speed doesn't change the actual
        /// speed of the animator, but a parameter of the same name that can be used to multiply
        /// the speed of certain animations. </summary>
        /// <param name="animation">The name of the animation state to be played</param>
        /// <param name="speed">The speed to be set</param>
        public void PlayAnimation(string animation, float speed = 1) {
            Animator.Play(animation);
            Animator.SetFloat("speed", speed);
        }
    }
} // namespace ActiveRagdoll