using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using WeaponSOs;

public class ChargeCounter : MonoBehaviour
{
    [SerializeField]private Image chargeCounterImage;
    public void Awake()
    {
        this.gameObject.SetActive(false);
        //_chargeCounterImage.image.
    }

    public void ChargableWeaponActive(WeaponSO weaponData)
    {
        this.gameObject.SetActive(enabled = weaponData.chargable);
        
    }

    public void UpdateCharge(float charge)
    {
        chargeCounterImage.fillAmount = charge/5f;
    }
    
   
}
