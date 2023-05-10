using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActiveRagdoll;

public class AIDefaultBehaviour : MonoBehaviour
{
    [Header("Modules")]
    [SerializeField] private ActiveRagdoll.ActiveRagdoll _activeRagdoll;
    [SerializeField] private AIForcedMovement _aiForcedMovement;
    [SerializeField] private AIAnimationModule _aiAnimationModule;

    [Header("Target")]
    [SerializeField] private Transform target;

    private void OnValidate()
    {
        if (_activeRagdoll == null) _activeRagdoll = GetComponent<ActiveRagdoll.ActiveRagdoll>();
        if (_aiForcedMovement == null) _aiForcedMovement = GetComponent<AIForcedMovement>();
        if (_aiAnimationModule == null) _aiAnimationModule = GetComponent<AIAnimationModule>();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        Vector3 ballDirection = target.position - transform.position;
        ballDirection.y = 0;

        // Rotate the AI to face the ball
        Quaternion targetRotation = Quaternion.LookRotation(ballDirection.normalized);
        float rotationSpeed = 5.0f; // Adjust the rotation speed as needed
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        // The modelForwardOffset is not needed if the model is oriented correctly in the Unity editor
        // If you still need it, uncomment the following two lines and adjust the offset as needed
        // Vector3 modelForwardOffset = new Vector3(0, 0, 0);
        // transform.Rotate(modelForwardOffset, Space.Self);

        _aiAnimationModule.MovementDirection = ballDirection.normalized;

        Vector3 movementDirection = new Vector3(_aiForcedMovement.Joint.velocity.x, 0, _aiForcedMovement.Joint.velocity.z);

        if (movementDirection == Vector3.zero)
        {
            _aiAnimationModule.Animator.SetBool("moving", false);
            return;
        }

        _aiAnimationModule.Animator.SetBool("moving", true);
        _aiAnimationModule.Animator.SetFloat("speed", movementDirection.magnitude);
    }
}
