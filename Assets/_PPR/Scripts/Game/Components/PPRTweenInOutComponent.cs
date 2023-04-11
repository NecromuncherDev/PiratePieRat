using DG.Tweening;
using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRTweenInOutComponent : PPRPoolable
	{
        [Header("Tweening")]
        [SerializeField] protected float tweenTimeIn = 1f;
        [SerializeField] protected float tweenTimeOut = 1f;
        [SerializeField] protected float scaleStart = 0f;
        [SerializeField] protected float scaleEnd = 1f;
        [SerializeField] protected AnimationCurve inCurve;
        [SerializeField] protected AnimationCurve outCurve;

        public void Init()
        {
            DoAnimationIn();
        }

        public virtual void DoAnimationIn()
        {
            transform.localScale = scaleStart * Vector3.one;

            transform.DOScale(scaleEnd * Vector3.one, tweenTimeIn).SetEase(inCurve);
        }

        public override void OnReturnedToPool()
        {
            transform.DOScale(scaleStart * Vector3.one, tweenTimeOut)
                .SetEase(outCurve)
                .OnComplete(() => base.OnReturnedToPool());
        }
    }
}
