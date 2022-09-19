using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthText : MonoBehaviour
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
