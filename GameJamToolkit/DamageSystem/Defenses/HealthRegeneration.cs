using System.Collections;
using UnityEngine;

namespace IceBlink.GameJamToolkit.DamageSystem.Defenses
{
    [RequireComponent(typeof(Damageable))]
    public class HealthRegeneration : MonoBehaviour
    {
        [field: SerializeField] public float TickInterval { get; private set; } = 0.5f;
        [field: SerializeField, Range(0.001f, 100f)] public float PercentMaxHealthToHeal { get; private set; } = 1f;
        [field: SerializeField] public float RegenDelay { get; private set; } = 3f;
        private bool ShouldHeal => Time.time - health.LastDamaged >= RegenDelay;

        private Health health;
        private WaitForSeconds tickDelay;
        private Coroutine regenRoutine;
        
        private void Awake()
        {
            health = GetComponent<Damageable>().Health;
            tickDelay = new WaitForSeconds(TickInterval);
        }

        private void OnEnable() => StartRegen();

        private void OnDisable() => StopRegen();

        private void StartRegen()
        {
            if(regenRoutine != null)
                StopCoroutine(regenRoutine);

            regenRoutine = StartCoroutine(RegenCoroutine());
        }

        private void StopRegen()
        {
            if(regenRoutine != null)
                StopCoroutine(regenRoutine);
        }

        private IEnumerator RegenCoroutine()
        {
            while (true)
            {
                while (!ShouldHeal)
                {
                    yield return null;
                    
                    if(health.IsDead)
                        yield break;
                }
            
                if(!health.IsFull)
                    health.Heal(health.Max * (PercentMaxHealthToHeal/100f));
                
                yield return tickDelay;
            }
        }
    }
}
