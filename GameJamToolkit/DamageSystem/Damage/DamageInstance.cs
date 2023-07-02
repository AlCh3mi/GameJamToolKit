using Random = UnityEngine.Random;

namespace IceBlink.GameJamToolkit.DamageSystem.Damage
{
    public struct DamageInstance
    {
        public float Amount { get; private set; }
        public bool IsCriticalHit { get; private set; }
        
        public IDamageDealer DamageDealer { get; private set; }

        public DamageInstance(IDamageDealer damageDealer)
        {
            DamageDealer = damageDealer;
            IsCriticalHit = IsACriticalHit(damageDealer.DamageProperties.CriticalHitChance);
            var critBonusDamage = damageDealer.DamageProperties.MinAttack * (damageDealer.DamageProperties.CriticalHitDamage / 100f);
            var amount = Random.Range(damageDealer.DamageProperties.MinAttack, damageDealer.DamageProperties.MaxAttack);
            Amount = IsCriticalHit
                ? amount + critBonusDamage
                : amount;
        }

        private static bool IsACriticalHit(float dmgCriticalHitChance)
        {
            var random = Random.Range(0f, 100f);
            return random <= dmgCriticalHitChance;
        }
    }
}