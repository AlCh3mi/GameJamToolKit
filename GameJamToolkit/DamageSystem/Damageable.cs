using System.Collections.Generic;
using System.Linq;
using IceBlink.GameJamToolkit.DamageSystem.Damage;
using IceBlink.GameJamToolkit.DamageSystem.Defenses;
using UnityEngine;
using UnityEngine.Events;

namespace IceBlink.GameJamToolkit.DamageSystem
{
    public class Damageable : MonoBehaviour
    {
        [Header("Defenses")]
        public float maxHealth = 100f;
        public List<Resistance> resistances;
        
        [Header("Events")]
        public UnityEvent<DamageReport> damaged;
        public UnityEvent death;
        public UnityEvent<float, float> healthChanged;

        private Health _health;
        private Defense _defense;
        private DamageOverTime _dot;
        
        public Health Health
        {
            get => _health;
            private set
            {
                _health = value;
                SignUpToHealthEvents();
            }
        }
        
        public Defense Defense
        {
            get => _defense;
            private set
            {
                _defense = value;
                SignUpToDefenseEvents();
            }
        }
        
        private DamageOverTime DamageOverTime
        {
            get
            {
                if (_dot != null)
                    return _dot;

                return _dot = TryGetComponent<DamageOverTime>(out var dot) 
                    ? dot 
                    : gameObject.AddComponent<DamageOverTime>();
            }
        }

        public bool IsDead => Health.IsDead;
        
        public DamageReport TakeDamage(IDamageDealer damageDealer)
        {
            if(Health.IsDead)
                return default;

            var damageInstance = new DamageInstance(damageDealer);
            var conflictResult = Defense.DefendAgainst(damageInstance);
            Health.TakeDamage(conflictResult.DamageAmount);
            damaged?.Invoke(conflictResult);
            return conflictResult;
        }

        public void AddDamageOverTime(float tickInterval, float duration, IDamageDealer damageDealer)
        {
            DamageOverTime.AddDotDamage(tickInterval, duration, damageDealer);
        }
        
        private void Awake()
        {
            Health = new Health(maxHealth, maxHealth);
            Defense = new Defense(resistances);
        }

        private void OnDisable()
        {
            Health.Death -= OnDeath;
            Health.HealthChanged -= OnHealthChanged;
            Defense.ResistanceUpdatedEvent -= OnResistanceUpdated;
        }

        private void SignUpToHealthEvents()
        {
            Health.Death -= OnDeath;
            Health.Death += OnDeath;
            Health.HealthChanged -= OnHealthChanged;
            Health.HealthChanged += OnHealthChanged;
        }
        
        private void SignUpToDefenseEvents()
        {
            Defense.ResistanceUpdatedEvent -= OnResistanceUpdated;
            Defense.ResistanceUpdatedEvent += OnResistanceUpdated;
        }
        
        private void OnHealthChanged(float curr, float max) => healthChanged?.Invoke(curr, max);

        private void OnDeath() => death?.Invoke();

        private void OnResistanceUpdated(Resistance obj)
        {
            if (resistances.Contains(obj))
            {
                var res = resistances.First(x => x.damageType == obj.damageType);
                if (Mathf.Approximately(res.amount,obj.amount))
                    return;

                res.amount = obj.amount;
            }
            
            resistances.Add(obj);
        }
    }
}
