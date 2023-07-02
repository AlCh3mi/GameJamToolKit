using UnityEngine;
using UnityEngine.UI;

namespace IceBlink.GameJamToolkit.DamageSystem.Indicators
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Damageable damageable;

        private void Awake()
        {
            if(!damageable)
                damageable = GetComponentInParent<Damageable>();
        }

        private void OnEnable()
        {
            slider.value = 0f;
            OnHealthChanged(damageable.Health.Current, damageable.Health.Max);
            damageable.healthChanged.AddListener(OnHealthChanged);
        }

        private void OnDisable() => damageable.healthChanged.RemoveListener(OnHealthChanged);

        private void OnHealthChanged(float current, float max) => slider.value = current / max;
    }
}
