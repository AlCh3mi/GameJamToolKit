using System.Collections.Generic;
using UnityEngine;

namespace IceBlink.GameJamToolkit.GameEvents
{
    [CreateAssetMenu(menuName = "Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> eventListeners = new();

        public virtual void Raise()
        {
            for(var i = eventListeners.Count -1; i >= 0; i--)
                eventListeners[i].OnEventRaised();
        }

        public virtual void RegisterListener(GameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public virtual void UnregisterListener(GameEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}