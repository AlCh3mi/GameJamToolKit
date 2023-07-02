using System;
using IceBlink.GameJamToolkit.DamageSystem.Damage;

namespace IceBlink.GameJamToolkit.DamageSystem.Defenses
{
    [Serializable]
    public struct Resistance
    {
        public DamageType damageType;
        public float amount;
    }
}