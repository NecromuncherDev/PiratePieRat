using DG.Tweening;
using PPR.Core;
using TMPro;
using UnityEngine;

namespace PPR.Game
{
    public class PPRTweenScoreComponent : PPRPoolable
    {
        [Header("References")]
        [SerializeField] private TMP_Text currencyTMP;
        [SerializeField] private SpriteRenderer currencyRenderer;

        [Header("Tweening")]
        [SerializeField] private float tweenTime = 1f;
        [SerializeField] private Vector3 moveAmount = Vector3.up;
        [SerializeField] private float fadeTarget = 0f;

        [SerializeField] private float scaleStart = 0f;
        [SerializeField] private float scaleEnd = 1f;

        [SerializeField] private Ease easeTypeMove = Ease.OutCubic;
        [SerializeField] private AnimationCurve fadeEase;

        public void Init(int amount, Sprite sprite, float horizontalVariation = 0f)
        {
            currencyTMP.text = $"+{amount:N0}";
            currencyRenderer.sprite = sprite;
            moveAmount.x = horizontalVariation;
            DoAnimation();
        }

        public void DoAnimation()
        {
            transform.localScale = scaleStart * Vector3.one;

            currencyTMP.DOFade(fadeTarget, tweenTime).SetEase(fadeEase);
            currencyRenderer.DOFade(fadeTarget, tweenTime).SetEase(fadeEase);

            transform.DOLocalMove(transform.localPosition + moveAmount, tweenTime).SetEase(easeTypeMove);
            transform.DOScale(scaleEnd * Vector3.one, tweenTime).OnComplete(() =>
            {
                Manager.PoolManager.ReturnPoolable(this);
            });
        }

        public override void OnReturnedToPool()
        {
            currencyTMP.color = currencyTMP.color.ResetAlpha();
            currencyRenderer.color = currencyRenderer.color.ResetAlpha();

            base.OnReturnedToPool();
        }
    }
}