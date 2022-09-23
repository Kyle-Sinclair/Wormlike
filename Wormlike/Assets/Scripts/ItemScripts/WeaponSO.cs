using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon",  menuName = "ScriptableObjects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [SerializeField]
    public bool Chargable;

    public string weaponName;
    [SerializeField] public GameObject weaponModel;

    [SerializeField] public GameObject bulletPrefab;
    
    
}
