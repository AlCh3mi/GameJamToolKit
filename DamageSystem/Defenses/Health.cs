using System;
using UnityEngine;

namespace IceBlink.DamageSystem.Defenses
{
    [Serializable]
    public class Health
    {
        [SerializeField] private float max = 100f;
        [field:SerializeField] public float InvulnerabilityPeriodOnDamaged { get; private set; }
        
        public event Action<float,float> HealthChanged;
        public event Action Damaged;
        public event Action Death;
    
    
        private float current;
        public float Current
        {
            get => current;
            private set
            {
                current = Mathf.Clamp(value, 0, Max);
                HealthChanged?.Invoke(current, Max);
            }
        }
    
        public float Max 
        { 
            get => max; 
            private set => max = Mathf.Clamp(value, 0, float.MaxValue); 
        }
    
        public bool IsFull => Current >= Max;
        public bool IsDead => Current <= 0 ;

        private bool Vulnerable => Time.time - LastDamaged >= InvulnerabilityPeriodOnDamaged;
        public float LastDamaged { get; private set; }

        public Health(Health health)
        {
            SetMaxHealth(health.Max);
            Current = health.Current;
            LastDamaged = Time.time - InvulnerabilityPeriodOnDamaged;
        }
        
        public Health(float max)
        {
            SetMaxHealth(max);
            Current = max;
            LastDamaged = Time.time - InvulnerabilityPeriodOnDamaged;
        }
        
        public Health(float current, float max)
        {
            SetMaxHealth(max);
            Current = current;
            LastDamaged = Time.time - InvulnerabilityPeriodOnDamaged;
        }

        public void SetMaxHealth(float newMax)
        {
            if (newMax <= 0)
                return;
        
            Max = newMax;
        
            if(Current > Max)
                Current = Max;
        }
    
        public void Heal(float healing)
        {
            if(healing <= 0)
                return;
        
            if(IsDead)
                return;

            Current += healing;
        }

        public void TakeDamage(float damage)
        {
            if(damage <= 0)
                return;
        
            if(!Vulnerable)
                return;
        
            if (IsDead)
                return;
        
            Current -= damage;
            LastDamaged = Time.time;
            Damaged?.Invoke();
        
            if(IsDead)
                Death?.Invoke();
        }
    }
}