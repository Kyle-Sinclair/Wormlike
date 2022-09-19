using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class ControllableCharacter : MonoBehaviour
{
    private PlayerInput _input;
    // Start is called before the first frame update
    private Vector2 _moveValue;
    [SerializeField] private float targetSpeed;
    private CharacterController _controller;

    //[SerializeField]private CinemachineVirtualCamera _vcam;
    private Vector3 moveVector;
    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(_moveValue);
        moveVector.x = _moveValue.x;
        moveVector.y = 0f;
        moveVector.z = _moveValue.y;
        _controller.Move(moveVector);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _moveValue = context.ReadValue<Vector2>();
        Debug.Log("move is being called");
        
    }

    public void ActivateAsControllable()
    {
        //Debug.Log("Activating one characters input");
        _input.ActivateInput();

    }
    public void DeactivateAsControllable()
    {
        _input.DeactivateInput();

    }
}
