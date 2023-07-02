using UnityEngine;

namespace Plugins.IceBlink.GameJamToolkit.StateMachine
{
    public abstract class State<T> : ScriptableObject where T : MonoBehaviour
    {
        protected T runner;

        public virtual void EnterState(T parent) => runner = parent;

        public virtual void GetInput() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void StateChangeConditionCheck() { }

        public virtual void ExitState() { }

        public virtual void OnDrawGizmosSelected() { }
        
        //2D or 3D Collision and Trigger Callbacks can be added here and in the state runner depending on which are needed if any.
    }
}