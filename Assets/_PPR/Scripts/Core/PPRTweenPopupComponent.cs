using DG.Tweening;
using UnityEngine;

namespace PPR.Core
{
    public class PPRTweenPopupComponent : PPRPopupComponentBase
    {
        [Header("Tweening")]
        [SerializeField] protected float startSize = 1f;
        [SerializeField] protected float endSize = 1f;
        [SerializeField] protected float inTweenDuration = 1f;
        [SerializeField] protected float outTweenDuration = 1f;
        [SerializeField] protected AnimationCurve inCurve;
        [SerializeField] protected AnimationCurve outCurve;

        public override void Init(PPRPopupData data)
        {
            base.Init(data);

            transform.localScale = startSize * Vector3.one;
            transform.DOScale(endSize * Vector3.one, inTweenDuration).SetEase(inCurve);
        }

        protected override void OnClosePopup()
        {
            transform.DOScale(startSize * Vector3.one, outTweenDuration).SetEase(outCurve).OnComplete(() =>
            {
                base.OnClosePopup();
            });
        }
    }
}