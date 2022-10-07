using StaticsAndUtilities;
using UIScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using WeaponSOs;
namespace ThirdPersonScripts
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(MoveBehaviour))]
    [RequireComponent(typeof(JumpAndGravityBehaviour))]
    public class WormController : MonoBehaviour
    {
        [SerializeField] private int _initialHealth = 100;
        [FormerlySerializedAs("isActivePlayer")] public bool IsActivePlayer;
        public int TeamMemberIndex { get; set; }
        [FormerlySerializedAs("weaponryController")] 
        public WeaponryController WeaponryController;
        [FormerlySerializedAs("health")] [SerializeField]
        private HealthComponent _health;
        private PlayerInput _input;
        private MoveBehaviour _moveController; 
        private JumpAndGravityBehaviour _jumpController;
        private CharacterController _characterController; 
        private Transform CameraTarget { get; set; }

        public void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();
            _moveController = GetComponent<MoveBehaviour>();
            _jumpController =  GetComponent<JumpAndGravityBehaviour>();
            WeaponryController = GetComponent<WeaponryController>();
            _health = GetComponentInChildren<HealthComponent>();
            _health.SetHealth(_initialHealth);
            CameraTarget = transform.Find("Camera Target").transform;
            DeactivateAsControllable();
        }
        private void OnEnable()
        {
            if (!IsActivePlayer)
            {
                _input.DeactivateInput();
            }
            _health.OnDeath += Die;
        }
        private void OnDisable()
        {
            _health.OnDeath -= Die;
        }
        public delegate void Death(WormController character);
        public Death OnDeath;
        public delegate void KilledWhileActive();
        public KilledWhileActive OnKilledWhileActive;
        public void FixedUpdate()
        {
            Move();
        }
        private void Move()
        {
            _characterController.Move(_moveController.XZContribution() + _jumpController.YContribution());
        }
        public void ActivateAsControllable()
        {
            IsActivePlayer = true;
            _input.ActivateInput();
        }
        public void DeactivateAsControllable()
        {
            IsActivePlayer = false;
            WeaponryController.ClearCharge();
            _input.DeactivateInput();
        }
        public Transform GetCameraTarget()
        {
            return CameraTarget;
        }
        public void EquipWeapon(WeaponSO weaponData)
        {
            WeaponryController.EquipWeapon(weaponData);
        }
        private void Die()
        {
            if (IsActivePlayer)
            {
                OnKilledWhileActive?.Invoke();
            }
            OnDeath?.Invoke(this);
            Destroy(this.gameObject);
        }
        
        public void SetTeamColor(int teamNumber)
        {
            var renderer = GetComponentInChildren<Renderer>();
            renderer.material.SetColor("_Color",UtilitiesManager.SetColor((TeamColorEnum)teamNumber));
        }
    }
}
