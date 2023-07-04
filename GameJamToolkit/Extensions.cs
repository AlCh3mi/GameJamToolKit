using UnityEngine;

namespace IceBlink.GameJamToolkit
{
    public static class Extensions
    {
        public static bool IsInLayerMask(this GameObject go, LayerMask layerMask) 
            => (layerMask.value & (1 << go.layer)) > 0;

        public static bool IsInLayerMask(this Transform transform, LayerMask layerMask) 
            => transform.gameObject.IsInLayerMask(layerMask);

        public static void Reset(this Rigidbody rigidbody)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}