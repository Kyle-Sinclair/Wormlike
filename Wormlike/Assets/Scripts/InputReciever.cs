using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReciever : MonoBehaviour
{
    // private PlayerInput _input;
    // private Vector2 _moveValue;
    // [Header("Player Grounded")]
    // [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    // public bool Grounded = true;
    //
    // [Tooltip("Useful for rough ground")]
    // public float GroundedOffset = -0.14f;
    //
    // [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    // public float GroundedRadius = 0.28f;
    //
    // [Tooltip("What layers the character uses as ground")]
    // public LayerMask GroundLayers;
    // public void ReadMove(InputAction.CallbackContext context)
    // {
    //     _moveValue = context.ReadValue<Vector2>();
    // }
    // public void ReadJump(InputAction.CallbackContext context)
    // {
    //     if (Grounded && context.phase == InputActionPhase.Performed)
    //     {
    //         _jumpRequested = true;
    //     }
    // }
    // public void ActivateAsControllable()
    // {
    //     _input.ActivateInput();
    // }
    // public void DeactivateAsControllable()
    // {
    //     _input.DeactivateInput();
    // }
    // private void GroundedCheck()
    // {
    //     Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
    //         transform.position.z);
    //     Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
    //         QueryTriggerInteraction.Ignore);
    // }
}
