using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon SO")]
public class WeaponSO : ScriptableObject, ISerializationCallbackReceiver
{

    public string weaponName;
    public ShotType shotType;
    public float fireRate;

    [SerializeField] public GameObject bulletPrefab;
    void Init()
    {
        
    }
    public void OnBeforeSerialize()
    {
        Init();
    }

    public void OnAfterDeserialize()
    {
        
    }

    public enum ShotType
    {
        Auto,
        Single,
        Burst,
        Cannon
    }
}
