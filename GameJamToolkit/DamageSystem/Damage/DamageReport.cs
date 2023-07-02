using IceBlink.GameJamToolkit.DamageSystem.Defenses;
using UnityEngine;

namespace IceBlink.GameJamToolkit.DamageSystem.Damage
{
    public struct DamageReport
    {
        public DamageInstance DamageInstance { get; private set; }
            
        public Defense Defense { get; private set; }
        public float DamageAmount { get; private set; }
        public float Blocked { get; private set; }

        public DamageReport(DamageInstance damageInstance, Defense defense)
        {
            DamageInstance = damageInstance;
            Defense = defense;
            Blocked = damageInstance.Amount * (defense.GetResistancePercentageVs(damageInstance.DamageDealer.DamageProperties.DamageType) / 100);
            DamageAmount = Mathf.Clamp(damageInstance.Amount - Blocked, 0, float.MaxValue);
        }
    }
}