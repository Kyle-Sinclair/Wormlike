using System.Collections;
using System.IO.Enumeration;
using Projectiles;
using Projectiles.ImpactBehaviours;
using Projectiles.ProjectileBehaviours;
using StaticsAndUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using WeaponSOs;

namespace ThirdPersonScripts
{
    public class WeaponryController : MonoBehaviour
    {
        private bool _weaponEquipped;
        private GameObject _weaponModel;
        private ObjectSpawner _spawner = default;
        private bool _charging;
        private float _charge;
        [SerializeField]private Transform weaponHoverPoint;
        private Transform _munitionOriginPoint;
        private WeaponSO _weaponData;
        private float _shotAngle;
        public void Awake()
        {
            _charge = 0f;
            _shotAngle = 90f;
            _spawner = FindObjectOfType<ObjectSpawner>();
            weaponHoverPoint = transform.Find("WeaponAttachmentPoint");
        }
        public void OnChangeAngle(InputAction.CallbackContext context)
        {
            if (context.performed)
            { 
                _shotAngle += context.ReadValue<float>() * 5;
                _shotAngle = Mathf.Clamp(_shotAngle,0.0f,90.0f);
                _weaponModel.transform.localRotation =  Quaternion.Euler(_shotAngle, 0f,0f);
            }
        }

        public void PrimaryFire(InputAction.CallbackContext context)
        {

            if (_weaponEquipped && _weaponData.chargable)
            {
                ShootChargable(context);
            }
            else if(_weaponEquipped && context.canceled)
            {
                Shoot(0);
            }
        }
        

        private void ShootChargable(InputAction.CallbackContext context)
        {
            if(context.performed) // the key has been pressed
            {
                _charging = true;
                StartCoroutine(ChargeWeapon());
            }
                
            if(context.canceled) //the key has been released
            {
                Debug.Log("Exiting charging coroutine");
                _charging = false;
                Shoot(_charge);
                _charge = 0;
            }
        }

        private IEnumerator ChargeWeapon()
        {
            while (_charging)
            {
                _charge += Time.deltaTime;
                yield return null;
            }
        }
        
        private void Shoot(float chargeValue = 0)
        {
            if (!_weaponEquipped){
                return;
            }
            Debug.Log("charge value = " + chargeValue);
            Projectile projectile = _spawner.GetProjectile(_weaponData.projectileModel);
            OrientProjectile(projectile);
            ProjectileBehaviour projectileMovementBehaviour  = projectile.AddBehavior(_weaponData.projectileBehaviour);
            projectileMovementBehaviour.Initialize(projectile,chargeValue,_weaponData.speed);
            ImpactBehaviour behaviour = projectile.AddBehavior(_weaponData.impactBehaviourType);
            behaviour.Initialize(_weaponData.damage,_weaponData.force,_weaponData.range,_spawner.impactEffectFactory);
        }

        private void OrientProjectile(Projectile projectile)
        {
            var position = _munitionOriginPoint.position;
            Vector3 direction = position - weaponHoverPoint.position;
            projectile.transform.position = position;
            projectile.direction = direction;
        }

        public void EquipWeapon(WeaponSO weaponData)
        {
            if (this._weaponData == weaponData)
            {
                return;
            }

            if (_weaponEquipped)
            {
                Destroy(_weaponModel);
            }
            this._weaponData = weaponData;
            _weaponModel = Instantiate(weaponData.weaponModel, this.transform, false);
            _weaponModel.transform.position = weaponHoverPoint.position;
            _weaponModel.transform.localRotation =  Quaternion.Euler(_shotAngle, 0f,0f);
            _munitionOriginPoint = _weaponModel.transform.Find("MunitionOriginTransform");
            _charging = false;
            _weaponEquipped = true;
        }
    }
}
