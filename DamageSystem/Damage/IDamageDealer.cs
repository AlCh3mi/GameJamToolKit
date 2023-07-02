using UnityEngine;

namespace IceBlink.DamageSystem.Damage
{
    public interface IDamageDealer
    {
        public DamageProperties DamageProperties { get; }
        public Transform Transform { get; }
    }
}