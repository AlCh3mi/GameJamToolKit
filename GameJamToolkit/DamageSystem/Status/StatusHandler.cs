using UnityEngine;

namespace IceBlink.GameJamToolkit.DamageSystem.Status
{
    public class StatusHandler : MonoBehaviour, IStatusAffected
    {
        public GameObject GameObject => gameObject;
    }

    public interface IStatusAffected
    {
        GameObject GameObject { get; }
    }
}