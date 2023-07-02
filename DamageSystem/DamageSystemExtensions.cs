using UnityEngine;

namespace IceBlink.DamageSystem
{
    public static class DamageSystemExtensions
    {
        public static bool IsInLayerMask(this GameObject go, LayerMask layerMask) 
            => (layerMask.value & (1 << go.layer)) > 0;

        public static bool IsInLayerMask(this Transform transform, LayerMask layerMask) 
            => transform.gameObject.IsInLayerMask(layerMask);
    }
}