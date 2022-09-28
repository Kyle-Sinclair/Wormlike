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
        private MoveBehaviour _moveController; 
        private JumpAndGravityBehaviour _jumpController;
        private WeaponryController _weaponryController;
        private CharacterController _characterController;
        [SerializeField]
        private HealthComponent Health;
        void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();
            _moveController = GetComponent<MoveBehaviour>();
            _jumpController =  GetComponent<JumpAndGravityBehaviour>();
            _weaponryController = GetComponent<WeaponryController>();
            Health = GetComponentInChildren<HealthComponent>();
            DeactivateAsControllable();

        }
    
        private void OnEnable()
        {
            GetComponentInChildren<HealthComponent>().onDeath += Die;
        }
        private void OnDisable()
        {
            GetComponentInChildren<HealthComponent>().onDeath -= Die;
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            UpdateState();
            Move();
        }
        private void Move()
        {
            _characterController.Move(_moveController.XZContribution() + _jumpController.YContribution());
        }

        private void UpdateState()
        {
      
        }
        public void ActivateAsControllable()
        {
            _input.ActivateInput();
        }
        public void DeactivateAsControllable()
        {
            _input.DeactivateInput();
        }

        private enum State
        {
            Moving,
            Shooting,
            Finishing,
            Waiting
        }

        public void EquipWeapon(WeaponSO weaponData)
        {
            _weaponryController.EquipWeapon(weaponData);
        }

        public void TakeDamage(int damage)
        {
            Debug.Log("Player says I'm taking this much damage " + damage);
            Health.TakeDamage(damage);
        }

        void Die()
        {
            Destroy(this.gameObject);
        }
    }
}
