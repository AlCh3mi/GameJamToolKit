using UnityEngine;

namespace IceBlink.GameJamToolkit.DamageSystem.Status
{
    //[CreateAssetMenu(fileName = "New Status Effect", menuName = "Damage System/Status Effect")]
    public abstract class StatusEffect : ScriptableObject, IStatusEffect
    {
        public abstract void ApplyStatus(IStatusAffected statusTarget);
    }

    public interface IStatusEffect
    {
        void ApplyStatus(IStatusAffected statusAffected);
    }
}