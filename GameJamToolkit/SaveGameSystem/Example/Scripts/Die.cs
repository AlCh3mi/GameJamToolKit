using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example
{
    public class Die : MonoBehaviour
    {
        public void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
