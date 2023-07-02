using UnityEngine;
using UnityEngine.UI;

namespace IceBlink.AudioSystem
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;

        private void Awake()
        {
            //this needs to be done to avoid a DivideByZero problem
            SetSliderMinAndMaxValues(masterSlider);
            SetSliderMinAndMaxValues(musicSlider);
            SetSliderMinAndMaxValues(effectsSlider);
        }

        private void OnEnable()
        {
            masterSlider.SetValueWithoutNotify(AudioManager.Instance.MasterVolume);
            musicSlider.SetValueWithoutNotify(AudioManager.Instance.MusicVolume);
            effectsSlider.SetValueWithoutNotify(AudioManager.Instance.EffectsVolume);

            masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            effectsSlider.onValueChanged.AddListener(OnEffectsVolumeChanged);
        }

        public void OnMasterVolumeChanged(float value) => AudioManager.Instance.SetMasterVolume(value);

        public void OnMusicVolumeChanged(float value) => AudioManager.Instance.SetMusicVolume(value);

        public void OnEffectsVolumeChanged(float value) => AudioManager.Instance.SetEffectsVolume(value);
        
        private void SetSliderMinAndMaxValues(Slider slider)
        {
            slider.minValue = 0.0001f;
            slider.maxValue = 1f;
        }
    }
}