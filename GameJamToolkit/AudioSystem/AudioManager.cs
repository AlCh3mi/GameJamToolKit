using IceBlink.GameJamToolkit.Singletons;
using UnityEngine;
using UnityEngine.Audio;

namespace IceBlink.GameJamToolkit.AudioSystem
{
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Default Sound Settings")]
        [Tooltip("It will retrieve players audio settings from PlayerPrefs at startup, if not the defaults will be applied")]
        [SerializeField] private bool rememberPlayerAudioSettings;
        [SerializeField, Range(0.001f, 1f)] private float defaultMasterVol = 1f;
        [SerializeField, Range(0.001f, 1f)] private float defaultMusicVol = 1f;
        [SerializeField, Range(0.001f, 1f)] private float defaultEffectsVol = 1f;

        [Header("Dependencies"), Space]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource effectsSource;
        [SerializeField] private AudioMixer mixer;
        
        //these strings need to match the exposed parameters on the AudioMixer
        private const string MASTERVOLUME = "MasterVol";
        private const string MUSICVOLUME = "MusicVol";
        private const string EFFECTSVOLUME = "EffectsVol";
    
        public float MasterVolume => PlayerPrefs.GetFloat(MASTERVOLUME, defaultMasterVol);

        public float MusicVolume => PlayerPrefs.GetFloat(MUSICVOLUME, defaultMusicVol);

        public float EffectsVolume => PlayerPrefs.GetFloat(EFFECTSVOLUME, defaultEffectsVol);

        public AudioClip CurrentMusic => musicSource.clip;

        public bool IsMusicPlaying => musicSource.isPlaying;

        private void Start() => SetMixerVolumeValues();

        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if(!musicSource)
                return;
            
            if(musicSource.isPlaying)
                musicSource.Stop();

            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }

        public void PauseMusic(bool pause)
        {
            if(!musicSource)
                return;
            
            if(pause)
                musicSource.Pause();
            else
                musicSource.UnPause();
        }
        
        public void StopMusic()
        {
            if(!musicSource)
                return;
            
            if(musicSource.isPlaying)
                musicSource.Stop();

            musicSource.time = 0f;
        }
        
        public void PlayOneShot(AudioClip clip)
        {
            if(effectsSource)
                effectsSource.PlayOneShot(clip);
        }

        public void SetMasterVolume(float value) => SetFloat(MASTERVOLUME, value);

        public void SetMusicVolume(float value) => SetFloat(MUSICVOLUME, value);

        public void SetEffectsVolume(float value) => SetFloat(EFFECTSVOLUME, value);

        private void SetFloat(string paramName, float value)
        {
            var logVol = Mathf.Log10(value) * 20;
            mixer.SetFloat(paramName, logVol);
            Debug.Log($"Setting {paramName} to : {value}");
            PlayerPrefs.SetFloat(paramName, value);
        }
        
        private void SetMixerVolumeValues()
        {
            SetMasterVolume(rememberPlayerAudioSettings ? MasterVolume : defaultMasterVol);
            SetMusicVolume(rememberPlayerAudioSettings ? MusicVolume : defaultMusicVol);
            SetEffectsVolume(rememberPlayerAudioSettings ? EffectsVolume : defaultEffectsVol);
        }
    }
}