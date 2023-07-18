using IceBlink.GameJamToolkit.DamageSystem.Damage;
using UnityEngine;

namespace IceBlink.GameJamToolkit.DamageSystem.Status
{
    [CreateAssetMenu(fileName = "New DoT Status Effect", menuName = "Damage System/Status Effect/DoT Damage")]
    public class DotStatusEffect : StatusEffect, IDamageDealer
    {
        [SerializeField] private float tickInterval = 1f;
        [SerializeField] private float duration = 4f;
        [field: SerializeField] public DamageProperties DamageProperties { get; private set; }
        
        public override void ApplyStatus(IStatusAffected statusTarget)
        {
            if(!statusTarget.GameObject.TryGetComponent(out Damageable damageable))
                return;
            
            damageable.AddDamageOverTime(tickInterval, duration, this);
        }
    }
}