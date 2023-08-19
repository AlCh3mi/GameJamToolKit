using System;
using IceBlink.GameJamToolkit.DamageSystem;
using IceBlink.GameJamToolkit.DamageSystem.Damage;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example
{
    public class ClickToDamage : MonoBehaviour, IDamageDealer
    {
        [SerializeField] private LayerMask affectedLayer;
        
        [field: SerializeField] public DamageProperties DamageProperties { get; private set; }
        
        private Camera mainCam;
        
        private void Awake() => mainCam = Camera.main;

        private void Update()
        {
            if(!Input.GetMouseButton(0))
                return;
            
            if (!Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out var hit, 100f, affectedLayer))
                return;

            if (!hit.transform.TryGetComponent<Damageable>(out var damageable))
                return;
            
            var report = damageable.TakeDamage(this);
        }
    }
}
