using TMPro;
using UnityEngine;

namespace UIScripts
{
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
            Debug.Log("Health component setting health to " + health);
            _currentHealth = health;
            _hitPoints.text = _currentHealth.ToString();
        
            if(health < 0)
            {
                onDeath?.Invoke();
            }
        }
        public void TakeDamage(int damage)
        {
            int newHealth = _currentHealth - damage;
            if (newHealth <= 0)
            {
                onDeath?.Invoke();
            }
            SetHealth(newHealth);
        }

        public delegate void OnDeath();
        public OnDeath onDeath;
    }
}
