using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HealthComponent : MonoBehaviour
{
    
    [SerializeField,Range(25,100)]private int _currentHealth;
    [SerializeField] private TextMeshPro _hitPoints;
    public void Awake()
    {
        SetHealth(_currentHealth);
    }
    public void SetHealth(int health)
    {
        _hitPoints.text = health.ToString();
    }
    public void TakeDamage(int damage)
    {
        int newHealth = _currentHealth - damage; 
        SetHealth(newHealth);
    }
}
