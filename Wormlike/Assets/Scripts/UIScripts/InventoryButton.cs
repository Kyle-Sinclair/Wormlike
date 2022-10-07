using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using WeaponSOs;

namespace UIScripts
{
    public class InventoryButton : MonoBehaviour
    {
        [FormerlySerializedAs("weaponData")] 
        [SerializeField]private WeaponSO _weaponData;
        [FormerlySerializedAs("mTextComponent")] 
        [SerializeField]private TextMeshProUGUI _mTextComponent;

        private void Awake()
        {

            if (_weaponData != null)
            {
                _mTextComponent.text = _weaponData.WeaponName;
            }
        }
    }
}
