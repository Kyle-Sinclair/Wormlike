using System.Collections;
using System.IO.Enumeration;
using ItemScripts;
using Projectiles;
using Projectiles.ImpactBehaviours;
using Projectiles.ProjectileBehaviours;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThirdPersonScripts
{
    public class WeaponryController : MonoBehaviour
    {
        private bool _weaponEquipped;
        private GameObject _weaponModel;
        private ObjectSpawner spawner = default;
        private bool _charging;
        private float _charge;
        [SerializeField]private Transform _weaponHoverPoint;
        private Transform _munitionOriginPoint;
        private WeaponSO _weaponData;
        private float _shotAngle;
        public void Awake()
        {
            _charge = 0f;
            _shotAngle = 90f;
            spawner = FindObjectOfType<ObjectSpawner>();
            _weaponHoverPoint = transform.Find("WeaponAttachmentPoint");
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

            if (_weaponEquipped && _weaponData.Chargable)
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
            Projectile projectile = spawner.GetProjectile(_weaponData.ProjectileModel);
            OrientProjectile(projectile);
            ProjectileBehaviour projectileMovementBehaviour  = projectile.AddBehavior(_weaponData.ProjectileBehaviour);
            projectileMovementBehaviour.Initialize(projectile,chargeValue,_weaponData.speed);
            ImpactBehaviour behaviour = projectile.AddBehavior(_weaponData.ImpactBehaviourType);
            behaviour.Initialize(_weaponData.damage,_weaponData.Force,_weaponData.Range,spawner._impactEffectFactory);
        }

        private void OrientProjectile(Projectile projectile)
        {
            var position = _munitionOriginPoint.position;
            Vector3 direction = position - _weaponHoverPoint.position;
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
            _weaponModel.transform.position = _weaponHoverPoint.position;
            _weaponModel.transform.localRotation =  Quaternion.Euler(_shotAngle, 0f,0f);
            _munitionOriginPoint = _weaponModel.transform.Find("MunitionOriginTransform");
            _charging = false;
            _weaponEquipped = true;
        }
    }
}
