using UnityEngine;

namespace UIScripts
{
    public class HealthTextRotation : MonoBehaviour
    {
        private Camera m_Camera;

        void Start()
        {
            m_Camera = Camera.main;
        }
        void Update()
        {
            transform.rotation = m_Camera.transform.rotation;
        
        }
    }
}
