using UnityEngine;
using UnityEngine.Pool;

namespace Plugins.IceBlink.GameJamToolkit.ObjectPooling
{
    public class Pool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected T prefab;
        [SerializeField] protected int defaultSize = 10;
        [SerializeField] protected int capacity = 50;

        private IObjectPool<T> pool;

        private void Start()
        {
            SetupPool();
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
        
        private void OnRetrieve(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        private void OnReturned(T obj)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
