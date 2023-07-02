using UnityEngine;
using UnityEngine.Events;

namespace IceBlink.GameJamToolkit.Common
{
    [RequireComponent(typeof(Collider))]
    public class TriggerCallbacks : MonoBehaviour
    {
        public UnityEvent<Collider> triggerEnter;
        public UnityEvent<Collider> triggerExit;
        public UnityEvent<Collider> triggerStay;

        public LayerMask layerMask;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.gameObject.IsInLayerMask(layerMask))
                triggerEnter?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.IsInLayerMask(layerMask))
                triggerStay?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.IsInLayerMask(layerMask))
                triggerExit?.Invoke(other);
        }
    }
}
