using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace IceBlink.GameJamToolkit
{
    //I added DoTween to the collection after making this file, so now im committed...
    //but it could probably be replaced with a DoFade oneliner...
    //thanks Vicot for pointing that out. -_- 
    public class FadeToBlack : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeTime = 2f;
        [SerializeField] private float delayFadeCompleted = 3f;

        public UnityEvent fadeToBlackCompleted;

        private Coroutine fadeRoutine;

        private void Awake()
        {
            if (!canvasGroup)
                canvasGroup = GetComponent<CanvasGroup>();
        }

        [ContextMenu("Fade To Black")]
        public void Fade()
        {
            if(!canvasGroup)
                return;
            
            if(fadeRoutine != null)
                StopCoroutine(fadeRoutine);
            
            fadeRoutine = StartCoroutine(FadeRoutine());
        }

        private IEnumerator FadeRoutine()
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
            fadeRoutine = null;
        }
    }
}
