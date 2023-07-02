using UnityEngine;

namespace Plugins.IceBlink.GameJamToolkit.ObjectPooling
{
    public class PoolableObject : MonoBehaviour
    {
        public delegate void ReturnToPoolEvent(PoolableObject poolableObject);
    
        public event ReturnToPoolEvent ReturnToPool;

        protected virtual void OnDisable()
        {
            ReturnToPool?.Invoke(this);
        }
    }
}
