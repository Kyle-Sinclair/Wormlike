using ItemScripts;
using TMPro;
using UnityEngine;

namespace UIScripts
{
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
}
