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
    private WeaponryController _weaponryController;
    private CharacterController _characterController;
    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
        _moveController = GetComponent<MoveBehaviour>();
        _jumpController =  GetComponent<JumpAndGravityBehaviour>();
        _weaponryController = GetComponent<WeaponryController>();
        DeactivateAsControllable();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateState();
        Move();
    }
    private void Move()
    {
        _characterController.Move(_moveController.XZContribution() + _jumpController.YContribution());
    }

    private void UpdateState()
    {
      
    }
    public void ActivateAsControllable()
    {
        _input.ActivateInput();
    }
    public void DeactivateAsControllable()
    {
        _input.DeactivateInput();
    }

    private enum State
    {
        Moving,
        Shooting,
        Finishing,
        Waiting
    }

    public void EquipWeapon(WeaponSO weaponData)
    {
        _weaponryController.EquipWeapon(weaponData);
    }
}
