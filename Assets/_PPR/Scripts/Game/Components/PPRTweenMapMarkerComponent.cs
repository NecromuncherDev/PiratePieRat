using DG.Tweening;
using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
	public class PPRTweenMapMarkerComponent : PPRPoolable
	{
        [SerializeField] private float tweenTime = 1f;

        [SerializeField] private float scaleStart = 0f;
        [SerializeField] private float scaleEnd = 1f;

        [SerializeField] private Ease easeTypeMove = Ease.InBounce;

        public void Init()
        {
            DoAnimation();
        }

        public void DoAnimation()
        {
            transform.localScale = scaleStart * Vector3.one;

            transform.DOScale(scaleEnd * Vector3.one, tweenTime);
        }

        public override void OnReturnedToPool()
        {
            base.OnReturnedToPool();
        }
    }
}