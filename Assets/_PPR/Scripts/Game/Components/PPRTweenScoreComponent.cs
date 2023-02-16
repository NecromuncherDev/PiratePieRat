using DG.Tweening;
using PPR.Core;
using TMPro;
using UnityEngine;

namespace PPR.Game
{
    public class PPRTweenScoreComponent : PPRPoolable
    {
        [SerializeField] private TMP_Text scoreTMP;

        [SerializeField] private float tweenTime = 1f;
        [SerializeField] private Vector3 moveAmount = Vector3.up;
        [SerializeField] private float fadeTarget = 0f;

        [SerializeField] private float scaleStart = 0f;
        [SerializeField] private float scaleEnd = 1f;

        [SerializeField] private Ease easeTypeMove = Ease.OutCubic;
        [SerializeField] private AnimationCurve fadeEase;

        public void Init(int amount)
        {
            scoreTMP.text = $"+{amount:N0}";
            DoAnimation();
        }

        public void DoAnimation()
        {
            transform.localScale = scaleStart * Vector3.one;

            scoreTMP.DOFade(fadeTarget, tweenTime).SetEase(fadeEase);
            transform.DOLocalMove(transform.localPosition + moveAmount, tweenTime).SetEase(easeTypeMove);
            transform.DOScale(scaleEnd * Vector3.one, tweenTime).OnComplete(() =>
            {
                Manager.PoolManager.ReturnPoolable(this);
            });
        }

        public override void OnReturnedToPool()
        {
            var tempColor = scoreTMP.color;
            tempColor.a = 1;
            scoreTMP.color = tempColor;
            base.OnReturnedToPool();
        }
    }
}