using UnityEngine;

namespace UIScripts
{
    public class HealthTextRotation : MonoBehaviour
    {
        private Camera _mCamera;

        void Start()
        {
            _mCamera = Camera.main;
        }
        void Update()
        {
            transform.rotation = _mCamera.transform.rotation;
        
        }
    }
}
