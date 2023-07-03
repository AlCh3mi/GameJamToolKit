using UnityEngine;

namespace IceBlink.GameJamToolkit
{
    public static class Extensions
    {
        public static bool IsInLayerMask(this GameObject go, LayerMask layerMask) 
            => (layerMask.value & (1 << go.layer)) > 0;

        public static bool IsInLayerMask(this Transform transform, LayerMask layerMask) 
            => transform.gameObject.IsInLayerMask(layerMask);
    }
}