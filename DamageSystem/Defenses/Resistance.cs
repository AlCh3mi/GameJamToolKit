using System;
using IceBlink.DamageSystem.Damage;

namespace IceBlink.DamageSystem.Defenses
{
    [Serializable]
    public struct Resistance
    {
        public DamageType damageType;
        public float amount;
    }
}