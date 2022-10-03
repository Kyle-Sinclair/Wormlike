using ItemScripts;
using UIScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThirdPersonScripts
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(MoveBehaviour))]
    [RequireComponent(typeof(JumpAndGravityBehaviour))]

    public class WormController : MonoBehaviour
    {
        private PlayerInput _input;
        public int TeamMemberIndex { get; set; }
        public bool IsActivePlayer;
        private MoveBehaviour _moveController; 
        private JumpAndGravityBehaviour _jumpController;
        private WeaponryController _weaponryController;
        private CharacterController _characterController;
        [SerializeField]
        private HealthComponent health;

        private Transform CameraTarget { get; set; }
        void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();
            _moveController = GetComponent<MoveBehaviour>();
            _jumpController =  GetComponent<JumpAndGravityBehaviour>();
            _weaponryController = GetComponent<WeaponryController>();
            health = GetComponentInChildren<HealthComponent>();
            CameraTarget = transform.Find("Camera Target").transform;
            DeactivateAsControllable();

        }
        // Update is called once per frame
        void FixedUpdate()
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
            _input.DeactivateInput();
        }
        public Transform GetCameraTarget()
        {
            return CameraTarget;
        }
        public void EquipWeapon(WeaponSO weaponData)
        {
            _weaponryController.EquipWeapon(weaponData);
        }
        void Die()
        {
            onDeath?.Invoke(this);
            Destroy(this.gameObject);
        }
        public delegate void OnDeath(WormController character);
        public OnDeath onDeath;
        
        private void OnEnable()
        {
            if (!IsActivePlayer)
            {
                
                _input.DeactivateInput();
            }
            health.onDeath += Die;
        }
        private void OnDisable()
        {
            health.onDeath -= Die;
        }
    }
}
