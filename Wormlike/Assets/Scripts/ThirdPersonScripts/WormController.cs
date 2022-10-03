using UIScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using WeaponSOs;

namespace ThirdPersonScripts
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(MoveBehaviour))]
    [RequireComponent(typeof(JumpAndGravityBehaviour))]

    public class WormController : MonoBehaviour
    {
        private PlayerInput _input;
        [SerializeField] private int _initialHealth = 100;
        public int TeamMemberIndex { get; set; }
        public bool isActivePlayer;
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
            health.SetHealth(_initialHealth);
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
            isActivePlayer = true;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * 200f, ForceMode.Impulse);
            _input.ActivateInput();
        }
        public void DeactivateAsControllable()
        {
            isActivePlayer = false;
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
            OnDeath?.Invoke(this);
            Destroy(this.gameObject);
        }
        public delegate void Death(WormController character);
        public Death OnDeath;
        
        private void OnEnable()
        {
            if (!isActivePlayer)
            {
                
                _input.DeactivateInput();
            }
            health.OnDeath += Die;
        }
        private void OnDisable()
        {
            health.OnDeath -= Die;
        }
    }
}
