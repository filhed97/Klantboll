using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActiveRagdoll;

public class AIDefaultBehaviour : MonoBehaviour
{
    [Header("Modules")]
    [SerializeField] private ActiveRagdoll.ActiveRagdoll _activeRagdoll;
    [SerializeField] private JensAiForcedMovement _aiForcedMovement;
    [SerializeField] private AIAnimationModule _aiAnimationModule;

    private void OnValidate()
    {
        if (_activeRagdoll == null) _activeRagdoll = GetComponent<ActiveRagdoll.ActiveRagdoll>();
        if (_aiForcedMovement == null) _aiForcedMovement = GetComponent<JensAiForcedMovement>();
        if (_aiAnimationModule == null) _aiAnimationModule = GetComponent<AIAnimationModule>();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        Vector3 movementDirection = new Vector3(_aiForcedMovement.Joint.velocity.x, 0, _aiForcedMovement.Joint.velocity.z);

        if (movementDirection == Vector3.zero)
        {
            _aiAnimationModule.Animator.SetBool("moving", false);
            return;
        }

        _aiAnimationModule.Animator.SetBool("moving", true);
        _aiAnimationModule.Animator.SetFloat("speed", movementDirection.magnitude);

        Vector3 targetForward = movementDirection.normalized;
        _aiAnimationModule.MovementDirection = targetForward;
    }
}
