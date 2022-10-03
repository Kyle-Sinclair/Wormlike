using UnityEngine;
using UnityEngine.InputSystem;
namespace ThirdPersonScripts
{
    [RequireComponent(typeof(CharacterController))]
    public class JumpAndGravityBehaviour : MonoBehaviour
    {
    
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;
        private Vector3 _yContribution;
        public float jumpHeight = 1.2f;
        public float gravity = -10.0f;
        private bool _jumpRequested = false;
        public bool grounded = true;
        public float groundedOffset = -0.14f;
        public float groundedRadius = 0.28f;
        public LayerMask groundLayers;
    
        public void FixedUpdate()
        {
            GroundedCheck();
        }
        public void ReadJump(InputAction.CallbackContext context)
        {
            if (grounded && context.phase == InputActionPhase.Performed)
            {
                _jumpRequested = true;
            }
        }
        private void ResetVerticalSpeed()
        {
            if (grounded && _verticalVelocity < 0)
            {
                _verticalVelocity = 0f;
            }
        }
        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset,
                transform.position.z);
            grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers,
                QueryTriggerInteraction.Ignore);
            ResetVerticalSpeed();
        }
        public Vector3 YContribution()
        {   
            Jump(); 
            AccelerateDueToGravity();
            return new Vector3(0,_verticalVelocity * Time.deltaTime,0) ;
        }

        private void Jump()
        {
            if (!_jumpRequested) return;
            _verticalVelocity += Mathf.Sqrt(-2f * gravity * jumpHeight);
            //Debug.Log(" setting request jump to false");
            _jumpRequested = false;
        }

        private void AccelerateDueToGravity()
        {
            if (grounded) return;
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += gravity * Time.deltaTime;
            }
        }
    }
}
