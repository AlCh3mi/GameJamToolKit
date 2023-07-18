using UnityEngine;

namespace IceBlink.GameJamToolkit
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] protected float radius = 0.5f;
        [SerializeField] protected Color color = Color.blue;
        protected void OnDrawGizmosSelected()
        {
            var tmpColor = Gizmos.color;
            var position = transform.position;
            Gizmos.color = color;
            Gizmos.DrawSphere(position, radius);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(position, position + (transform.forward * radius));
            Gizmos.color = tmpColor;
        }
    }
}