using System;
using UnityEngine;

namespace IceBlink.GameJamToolkit.DamageSystem.Damage
{
    [Serializable]
    public class DamageProperties
    {
        [field: SerializeField] public float MinAttack { get; private set;}
        [field: SerializeField] public float MaxAttack { get; private set;}
        [field: SerializeField] public DamageType DamageType { get; private set;}
        [field: SerializeField] public float CriticalHitChance { get; private set;}
        [field: SerializeField] public float CriticalHitDamage { get; private set;}
    }
}