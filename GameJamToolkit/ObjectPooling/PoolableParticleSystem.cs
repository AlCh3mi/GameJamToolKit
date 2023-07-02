using UnityEngine;

namespace Plugins.IceBlink.GameJamToolkit.ObjectPooling
{
    [RequireComponent(typeof(ParticleSystem))]
    public class PoolableParticleSystem : PoolableObject
    {
        public ParticleSystem ParticleSys { get; private set; }

        private void Awake()
        {
            ParticleSys = GetComponent<ParticleSystem>();
            var main = ParticleSys.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnEnable()
        {
            ParticleSys.Play();
        }

        private void OnParticleSystemStopped()
        {
            gameObject.SetActive(false);
        }
    }
}
