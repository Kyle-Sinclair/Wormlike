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
        private Projectile _bulletPrefab;
        private ProjectileBehaviourType _projectileBehaviourType;
        private ImpactBehaviourType _impactBehaviourType;
        private ObjectSpawner spawner = default;
        public ImpactEffectFactory _impactEffectFactory = default;
        private bool _isChargable;
        private bool _charging;
        private float _charge;
        [SerializeField] public Transform _weaponHoverPoint;
        [SerializeField] public Transform _munitionOriginPoint;
        private WeaponSO _weaponData;
        private float _shotAngle;

        public void Awake()
        {
            _charge = 0f;
            _shotAngle = 45f;
            spawner = FindObjectOfType<ObjectSpawner>();
      
        }
        public void OnChangeAngle(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("Entering Change angle");
                _shotAngle += context.ReadValue<float>() * 5;
                _shotAngle = Mathf.Clamp(_shotAngle,0.0f,90.0f);
                //print(_shotAngle);
                //_weaponModel.transform.localRotation = Quaternion.Euler(_shotAngle, _weaponModel.transform.eulerAngles.y,  _weaponModel.transform.eulerAngles.z);
                _weaponModel.transform.localRotation =  Quaternion.Euler(_shotAngle, 0f,0f);
                //print(_weaponModel.name);
            }
        }
    
        public void PrimaryFire(InputAction.CallbackContext context)
        {
            if (!_weaponEquipped)
            {
                return;
            }
            if (_isChargable)
            {
                FireChargable(context);
            }
            else
            {
                FireNonChargable();
            }
        }

        private void FireNonChargable()
        {
        
        }
        private void FireChargable(InputAction.CallbackContext context)
        {
            if(context.performed) // the key has been pressed
            {
                _charging = true;
                StartCoroutine(ChargeWeapon());
            }
            if(context.canceled) //the key has been released
            {
                _charging = false;
                ShootChargable(_charge);
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

        private void ShootChargable(float chargeValue)
        {
            Projectile rocket = Instantiate(_bulletPrefab);
            Vector3 direction = _munitionOriginPoint.position - _weaponHoverPoint.position;
            rocket.direction = direction;
            rocket.transform.position = _munitionOriginPoint.transform.position;
            rocket.AddBehavior(_projectileBehaviourType);
            ImpactBehaviour behaviour = rocket.AddBehavior(_impactBehaviourType);
            behaviour.Initialize(_weaponData.Damage,_weaponData.Force,_weaponData.Range,spawner._impactEffectFactory);
        }
    
        private void OnEnable()
        {
    
        }

        public void EquipWeapon(WeaponSO _weaponData)
        {
            if (this._weaponData == _weaponData)
            {
                return;
            }
            this._weaponData = _weaponData;
            _weaponModel = Instantiate(_weaponData.weaponModel);
            _weaponModel.transform.parent = this.transform;
            _bulletPrefab = _weaponData.bulletPrefab;
            _weaponModel.transform.position = _weaponHoverPoint.position;
            _projectileBehaviourType = _weaponData.ProjectileBehaviour;
            _munitionOriginPoint = _weaponModel.transform.Find("MunitionOriginTransform");
            _isChargable = _weaponData.Chargable;
            _charging = false;
            _weaponEquipped = true;
        }
    }
}
