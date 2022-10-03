using UnityEngine;
using UnityEngine.InputSystem;

namespace ThirdPersonScripts
{
    public class MoveBehaviour : MonoBehaviour
    {
        private Vector2 _moveValue;
        private Vector3 _moveVector;
        [SerializeField] private float speed;
        [SerializeField] private Transform mainCamTransform;
        [SerializeField] private float turnSmoothFactor;
        private float _turnSmoothVelocity;
    
        // Start is called before the first frame update
        void Start()
        {
            mainCamTransform = Camera.main.transform;
        }
        public Vector3 XZContribution()
        {
            if (_moveValue == Vector2.zero)
            {
                return Vector3.zero;
            }
            _moveVector = new Vector3(_moveValue.x, 0, _moveValue.y);
            float targetAngle = Mathf.Atan2(_moveVector.x, _moveVector.z) * Mathf.Rad2Deg +
                                mainCamTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                turnSmoothFactor);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        
            Vector3 moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
            return moveDir * (speed * Time.deltaTime);
        }
    
    
        public void ReadMove(InputAction.CallbackContext context)
        {
            _moveValue = context.ReadValue<Vector2>();
        }
    }
}
