using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponryController : MonoBehaviour
{
    private bool _weaponEquipped;

    private GameObject _weaponModel;
    private GameObject _bulletPrefab;
    private bool _isChargable;
    private bool _charging;
    private float _charge;
    [SerializeField] public Transform _weaponHoverPoint;
    private float _shotAngle;

    public void Awake()
    {
        _charge = 0f;
        _shotAngle = 45f;
    }

    public void Update()
    {
        
    }

    public void OnChangeAngle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Entering Change angle");
            _shotAngle += context.ReadValue<float>() * 5;
            _shotAngle = Mathf.Clamp(_shotAngle,0.0f,90.0f);
            //print(_shotAngle);
            //_weaponModel.transform.localRotation = Quaternion.Euler(_shotAngle, _weaponModel.transform.eulerAngles.y,  _weaponModel.transform.eulerAngles.z);
            _weaponModel.transform.localRotation =  Quaternion.Euler(_shotAngle, 0f,0f);
            //print(_weaponModel.name);
        }
    }
    
    public void PrimaryFire(InputAction.CallbackContext context)
    {
        if(context.performed) // the key has been pressed
        {
            _charging = true;
            StartCoroutine(ChargeWeapon());
        }
        if(context.canceled) //the key has been released
        {
            _charging = false;
            
            Debug.Log("charge reached" + _charge);
            _charge = 0;
        }
    }

    private IEnumerator ChargeWeapon()
    {
        while (_charging)
        {
            _charge += 1;
            yield return null;

        }
    }

    private void ShootChargable(float chargeValue)
    {
        GameObject rocket = Instantiate(_bulletPrefab);
        rocket.transform.position = _weaponHoverPoint.position;
        Rigidbody rb = rocket.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0,_shotAngle,0) * chargeValue,ForceMode.Impulse);
    }


    private void OnEnable()
    {
    
    }

    public void EquipWeapon(WeaponSO _weaponData)
    {
        _weaponModel = Instantiate(_weaponData.weaponModel);
        _weaponModel.transform.parent = this.transform;
        _bulletPrefab = _weaponData.bulletPrefab;
        _weaponModel.transform.position = _weaponHoverPoint.position;
        _isChargable = _weaponData.Chargable;
        if (_isChargable)
        {
             //_weaponModel.transform.Rotate(_shotAngle, 0.0f, 0.0f, Space.Self);
        }
        _charging = false;
        _weaponEquipped = true;
    }
}
