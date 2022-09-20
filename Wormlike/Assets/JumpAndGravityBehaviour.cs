using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class JumpAndGravityBehaviour : MonoBehaviour
{
    
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;
    private Vector3 _yContribution;
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -10.0f;

    private bool _jumpRequested = false;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;

    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;

    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.28f;

    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;
    // Start is called before the first frame update
    
    public void Update()
    {
        GroundedCheck();
    }
    public void ReadJump(InputAction.CallbackContext context)
    {
        if (Grounded && context.phase == InputActionPhase.Performed)
        {
            _jumpRequested = true;
        }
    }
    private void ResetVerticalSpeed()
    {
        if (Grounded && _verticalVelocity < 0)
        {
            _verticalVelocity = 0f;
        }
    }
    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);
        if (Grounded && _verticalVelocity < 0)
        {
            _verticalVelocity = 0f;
        }
    }
    public Vector3 YContribution()
    {   
        Jump(); 
        AccelerateDueToGravity();
        return new Vector3(0,_verticalVelocity * Time.deltaTime,0) ;
    }
    void Jump()
    {
        if (!_jumpRequested) return;
        _verticalVelocity += Mathf.Sqrt(-2f * Gravity * JumpHeight);
        //Debug.Log(" setting request jump to false");
        _jumpRequested = false;
    }

    void AccelerateDueToGravity()
    {
        if (Grounded) return;
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }
}
