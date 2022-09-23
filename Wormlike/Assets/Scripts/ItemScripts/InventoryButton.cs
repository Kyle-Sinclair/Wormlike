using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    [SerializeField]private WeaponSO _weaponData;
    [SerializeField]private TextMeshProUGUI m_TextComponent;

    void Awake()
    {

        if (_weaponData != null)
        {
            Debug.Log(("altering button text"));
            m_TextComponent.text = _weaponData.weaponName;
        }
    }
    private void OnEnable()
    {
        if (_weaponData != null)
        {
            Debug.Log(("altering button text"));
        }
    }
}
