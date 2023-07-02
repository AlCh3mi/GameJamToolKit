using UnityEngine;

namespace IceBlink.GameJamToolkit.AudioSystem.Example.Scripts
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private bool loop = true;
    
        public void Play() => AudioManager.Instance.PlayMusic(clip, loop);

        public void Pause() => AudioManager.Instance.PauseMusic(AudioManager.Instance.IsMusicPlaying);

        public void Stop() => AudioManager.Instance.StopMusic();
    }
}
