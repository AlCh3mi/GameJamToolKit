using IceBlink.GameJamToolkit.DamageSystem.Damage;
using UnityEngine;

namespace IceBlink.GameJamToolkit.DamageSystem.Indicators
{
    [RequireComponent(typeof(Damageable))]
    public class DamageIndicatorListener : MonoBehaviour
    {
        [SerializeField] private Vector3 spawnOffset = new (0, 2, 0);
    
        private Damageable damageable;

        private void Awake() => damageable = GetComponent<Damageable>();

        private void OnEnable() => damageable.damaged.AddListener(SpawnIndicator);

        private void OnDisable() => damageable.damaged.RemoveListener(SpawnIndicator);

        private void SpawnIndicator(DamageReport damage) => DamageIndicatorPool.Instance.SpawnIndicator(transform.position + spawnOffset, damage);
    }
}
