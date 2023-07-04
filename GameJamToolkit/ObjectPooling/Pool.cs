using System;
using UnityEngine;
using UnityEngine.Pool;

namespace IceBlink.GameJamToolkit.ObjectPooling
{
    public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected T prefab;
        [SerializeField] protected int defaultSize = 10;
        [SerializeField] protected int capacity = 50;
        [SerializeField] protected bool prewarm = true;
        
        private IObjectPool<T> pool;

        public IObjectPool<T> GetPool => pool;

        private void Awake()
        {
            SetupPool();
            
            if(!prewarm)
                return;

            Prewarm();
        }

        private void Prewarm()
        {
            var cache = new T[defaultSize];

            for (var i = 0; i < cache.Length; i++)
                cache[i] = pool.Get();

            foreach (var item in cache)
                pool.Release(item);
        }

        private void SetupPool()
        {
            pool = new ObjectPool<T>(() =>
                {
                    var instance = Instantiate(prefab, transform);

                    var poolableObj = instance.TryGetComponent<PoolableObject>(out var poolableObject)
                        ? poolableObject
                        : instance.gameObject.AddComponent<PoolableObject>();

                    poolableObj.ReturnToPool += (poolable) =>
                        pool.Release(poolable.GetComponent<T>());
                    
                    return instance;
                },
                OnRetrieve,
                OnReturned,
                (x) => Destroy(x.gameObject),
                false,
                defaultSize, 
                capacity);
        }
        
        protected virtual void OnRetrieve(T obj) => obj.gameObject.SetActive(true);

        protected virtual void OnReturned(T obj) => obj.gameObject.SetActive(false);
    }
}
