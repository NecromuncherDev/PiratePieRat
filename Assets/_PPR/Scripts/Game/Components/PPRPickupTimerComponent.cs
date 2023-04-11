using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PPR.Game
{
    public class PPRPickupTimerComponent : PPRTweenInOutComponent 
    {
        [SerializeField] private Image fillImage;
        private float timerDuration;

        public void Init(float duration)
        {
            timerDuration = duration;
            DoAnimationIn();
        }

        public override void DoAnimationIn()
        {
            base.DoAnimationIn();
            fillImage.fillAmount = 0;
            fillImage.DOFillAmount(1, timerDuration).SetEase(Ease.Linear).OnComplete(() => 
            {
                transform.DOScale(scaleStart * Vector3.one, tweenTimeOut)
                .SetEase(outCurve)
                .OnComplete(() => Manager.PoolManager.ReturnPoolable(this));
            });
        }
    }
}
