using UnityEngine;
using UnityEngine.Events;

namespace Plugins.IceBlink.GameJamToolkit.GameEvents
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private UnityEvent response;
        
        private void OnEnable() => gameEvent.RegisterListener(this);

        private void OnDisable() => gameEvent.UnregisterListener(this);

        public virtual void OnEventRaised() => response.Invoke();
    }
}