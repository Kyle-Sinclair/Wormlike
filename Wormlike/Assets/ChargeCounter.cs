using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WeaponSOs;

public class ChargeCounter : MonoBehaviour
{
    [FormerlySerializedAs("chargeCounterImage")] 
    [SerializeField]private Image _chargeCounterImage;
    public void Awake()
    {
        gameObject.SetActive(false);
    }
    public void ChargableWeaponActive(WeaponSO weaponData)
    {
        gameObject.SetActive(enabled = weaponData.Chargable);
    }
    public void UpdateCharge(float charge)
    {
        _chargeCounterImage.fillAmount = charge/5f;
    }
    public void ResetCharge()
    {
        _chargeCounterImage.fillAmount = 0f;
    }
    
}
