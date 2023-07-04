using System.Collections;
using UnityEngine;

namespace IceBlink.GameJamToolkit.DamageSystem.Damage
{
    [RequireComponent(typeof(Damageable))]
    public class DamageOverTime : MonoBehaviour
    {
        private Damageable damageable;
        
        private void Awake()
        {
            damageable = GetComponent<Damageable>();
        }

        public void AddDotDamage(float tickInterval, float duration, IDamageDealer damageDealer)
        {
            StartCoroutine(DotRoutine(tickInterval, duration, damageDealer));
        }

        private IEnumerator DotRoutine(float tickInterval, float duration, IDamageDealer damageDealer)
        {
            if(damageable.Health.IsDead)
                yield break;
            
            var delay = new WaitForSeconds(tickInterval);
            
            while (duration >= 0)
            {
                if (damageable.Health.IsDead)
                    yield break;
                
                damageable.TakeDamage(damageDealer);
                
                yield return delay;
                duration -= tickInterval;
            }
        }
    }
}
