using TMPro;
using UnityEngine;

namespace UIScripts
{
    public class HealthComponent : MonoBehaviour
    {
    
        [SerializeField,Range(25,100)]private int currentHealth;
        [SerializeField] private TextMeshPro hitPoints;
        public void Awake()
        {
            SetHealth(currentHealth);
        }
        public void SetHealth(int health)
        {
            //Debug.Log("Health component setting health to " + health);
            currentHealth = health;
            hitPoints.text = currentHealth.ToString();
            if(health < 0)
            {
                OnDeath?.Invoke();
            }
        }
        public void TakeDamage(int damage)
        {
            var newHealth = currentHealth - damage;
            if (newHealth <= 0)
            {
                OnDeath?.Invoke();
            }
            SetHealth(newHealth);
        }

        public delegate void Death();
        public Death OnDeath;
    }
}
