using UnityEngine;

namespace ItemScripts
{
    public class Weapon : MonoBehaviour
    {
        private WeaponSO _weaponData;
        private GameObject _weaponModel;
        public Weapon(WeaponSO _weaponData)
        {
            this._weaponData = _weaponData;
        
        }
    
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
