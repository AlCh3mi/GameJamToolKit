using UnityEngine;

namespace IceBlink.GameJamToolkit.StateMachine
{
    public abstract class StateRunner<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected State<T> startingState;
        [SerializeField] private bool showOnDrawGizmosSelected = true;
                
        protected State<T> _activeState;

        /// <summary>
        /// Switches to starting State
        /// </summary>
        protected virtual void Awake() => SwitchState(startingState);

        public void SwitchState(State<T> newState)
        {
            if (_activeState != null)
                _activeState.ExitState();

            _activeState = Instantiate(newState);
            _activeState.EnterState(GetComponent<T>());
        }

        protected virtual void Update()
        {
            _activeState.GetInput();
            _activeState.Update();
            _activeState.StateChangeConditionCheck();
        }

        protected virtual void FixedUpdate() => _activeState.FixedUpdate();

        private void OnDrawGizmosSelected()
        {
            if(!showOnDrawGizmosSelected)
                return;
            
            if(_activeState != null)
                _activeState.OnDrawGizmosSelected();
        }
    }
}