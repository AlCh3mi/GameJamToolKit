using System;
using UnityEngine;

namespace IceBlink.GameJamToolkit.DamageSystem.Damage
{
    [CreateAssetMenu(fileName = "New Damage Properties", menuName = "Damage System/Damage Properties")]
    public class DamageProperties : ScriptableObject
    {
        [field: SerializeField] public float MinAttack { get; private set;}
        [field: SerializeField] public float MaxAttack { get; private set;}
        [field: SerializeField] public DamageType DamageType { get; private set;}
        [field: SerializeField, Range(0, 100)] public float CriticalHitChance { get; private set;}
        [field: SerializeField] public float CriticalHitDamage { get; private set;}
    }
    
    public interface IDamageDealer
    {
        public DamageProperties DamageProperties { get; }
    }
}
