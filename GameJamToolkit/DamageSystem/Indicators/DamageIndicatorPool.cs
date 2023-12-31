﻿using IceBlink.GameJamToolkit.DamageSystem.Damage;
using IceBlink.GameJamToolkit.Singletons;
using UnityEngine;
using UnityEngine.Pool;

namespace IceBlink.GameJamToolkit.DamageSystem.Indicators
{
    public class DamageIndicatorPool : Singleton<DamageIndicatorPool>
    {
        [SerializeField] private DamageIndicator damageIndicatorPrefab;
        
        private IObjectPool<DamageIndicator> pool;
    
        public void SpawnIndicator(Vector3 worldPos, DamageReport damageInstance)
        {
            var indicator = pool.Get();
            indicator.transform.position = worldPos;
            indicator.SetDamage(damageInstance);
        }

        private void Start()
        {
            pool = new ObjectPool<DamageIndicator>(
                () =>
                {
                    var indicator = Instantiate(damageIndicatorPrefab, transform);
                    indicator.SetPool(pool);
                    return indicator;
                },
                x => x.gameObject.SetActive(true),
                x => x.gameObject.SetActive(false),
                x => Destroy(x.gameObject),
                false);
        }
    }
}
