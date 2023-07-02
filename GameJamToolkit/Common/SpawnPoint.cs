using UnityEngine;

namespace IceBlink.GameJamToolkit.Common
{
    public class SpawnPoint : MonoBehaviour
    {
        private void OnDrawGizmosSelected()
        {
            var position = transform.position;
            Gizmos.DrawSphere(position, 0.25f);
            Gizmos.DrawLine(position, position + transform.forward);
        }
    }
}