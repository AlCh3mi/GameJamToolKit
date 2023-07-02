using DG.Tweening;
using IceBlink.DamageSystem.Damage;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace IceBlink.DamageSystem.Indicators
{
    public class DamageIndicator : MonoBehaviour
    {
        [SerializeField] private float floatHeight = 1f;
        [SerializeField] private float duration = 1f;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color criticalHitColor;
        [SerializeField] private TMP_Text dmgText;
        [SerializeField] private Image damageTypeIndicator;
        [SerializeField] private CanvasGroup canvasGroup;

        private IObjectPool<DamageIndicator> pool;

        public void SetPool(IObjectPool<DamageIndicator> objPool) => pool = objPool;
    
        public void SetDamage(DamageReport damageReport)
        {
            if(damageTypeIndicator)
                damageTypeIndicator.sprite = damageReport.DamageInstance.DamageDealer.DamageProperties.DamageType.Icon;
        
            dmgText.color = damageReport.DamageInstance.IsCriticalHit ? criticalHitColor : defaultColor;
            var damageString = damageReport.DamageAmount;
            dmgText.text = damageString.ToString("0.0");
            
            ShowDamageIndicator();
        }

        private void ShowDamageIndicator()
        {
            canvasGroup.alpha = 1f;

            transform.localScale = Vector3.one * 0.5f;
            transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutElastic);

            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOMoveY(transform.position.y + floatHeight, duration).SetEase(Ease.OutQuad));
            sequence.Append(canvasGroup.DOFade(0, 1f).SetEase(Ease.OutQuad));

            sequence.OnComplete(() =>
            {
                if(pool != null)
                    pool.Release(this);
                else Destroy(gameObject);
            });
        }
    }
}
