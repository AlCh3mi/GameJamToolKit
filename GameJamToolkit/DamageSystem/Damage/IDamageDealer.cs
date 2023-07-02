using UnityEngine;

namespace IceBlink.GameJamToolkit.DamageSystem.Damage
{
    public interface IDamageDealer
    {
        public DamageProperties DamageProperties { get; }
        public Transform Transform { get; }
    }
}