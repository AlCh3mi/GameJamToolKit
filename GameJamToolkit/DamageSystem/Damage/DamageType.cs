using UnityEngine;

namespace IceBlink.GameJamToolkit.DamageSystem.Damage
{
    [CreateAssetMenu(fileName = "New DamageType", menuName = "Damage System/New Damage Type")]
    public class DamageType : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }

        public override string ToString() => name;
    }
}