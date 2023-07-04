using IceBlink.GameJamToolkit.DamageSystem.Damage;
using UnityEngine;

namespace IceBlink.GameJamToolkit.DamageSystem.Example
{
    public class DamageTester : MonoBehaviour, IDamageDealer
    {
        [SerializeField] private bool isTestingDotDamage;
        [SerializeField] private LayerMask layerMask;
        [field: SerializeField] public DamageProperties DamageProperties { get; private set; }
        
        public Transform Transform => transform;
    
        private Camera mainCam;
        private void Awake()
        {
            mainCam = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;
            
            var ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit, float.MaxValue, layerMask, QueryTriggerInteraction.Ignore))
                return;

            if (!hit.collider.gameObject.TryGetComponent<Damageable>(out var enemy))
                return;

            if(isTestingDotDamage)
                enemy.AddDamageOverTime(0.5f, 5f, this);
            else
                enemy.TakeDamage(this);
        }
    }
}
