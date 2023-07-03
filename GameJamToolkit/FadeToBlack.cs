using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace IceBlink.GameJamToolkit
{
    public class FadeToBlack : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeTime = 2f;
        [SerializeField] private float delayFadeCompleted = 3f;

        public UnityEvent fadeToBlackCompleted;

        public void StartDeathSequence() => StartCoroutine(DeathSequence());

        private IEnumerator DeathSequence()
        {
            var elapsed = 0f;
            while (elapsed <= fadeTime)
            {
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeTime);
                yield return null;
                elapsed += Time.deltaTime;
            }

            yield return new WaitForSeconds(delayFadeCompleted);
            
            fadeToBlackCompleted?.Invoke();
        }
    }
}
