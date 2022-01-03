using UnityEngine;

namespace Core.Combat
{
    public class Health
    {
        private float MaxHealth { get; }

        public float CurrentHealth { get; private set; }

        public Health(float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }

        public void ReceiveDamage(float damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        }
    }
}