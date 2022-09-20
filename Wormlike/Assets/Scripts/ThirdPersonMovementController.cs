using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(JumpAndGravityBehaviour))]

public class ThirdPersonMovementController : MonoBehaviour
{
    private PlayerInput _input;
    private MoveBehaviour _moveController; 
    private JumpAndGravityBehaviour _jumpController;
    private CharacterController _characterController;
    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
        _moveController = GetComponent<MoveBehaviour>();
        _jumpController = GetComponent<JumpAndGravityBehaviour>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {   
        Move();
    }
    private void Move()
    {
        _characterController.Move(_moveController.XZContribution() + _jumpController.YContribution());
    }
    public void ActivateAsControllable()
    {
        _input.ActivateInput();
    }
    public void DeactivateAsControllable()
    {
        _input.DeactivateInput();
    }
}
