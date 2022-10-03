using TMPro;
using UnityEngine;
using WeaponSOs;

namespace UIScripts
{
    public class InventoryButton : MonoBehaviour
    {
        [SerializeField]private WeaponSO weaponData;
        [SerializeField]private TextMeshProUGUI mTextComponent;

        void Awake()
        {

            if (weaponData != null)
            {
                Debug.Log(("altering button text"));
                mTextComponent.text = weaponData.weaponName;
            }
        }
        private void OnEnable()
        {
            if (weaponData != null)
            {
                Debug.Log(("altering button text"));
            }
        }
    }
}
