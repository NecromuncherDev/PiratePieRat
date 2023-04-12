using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PPR.Core
{
    public class PPRWelcomePopupComponent : PPRPopupComponentBase
    {
        [Header("Tweening")]
        [SerializeField] private float inTweenDuration = 1f;
        [SerializeField] private float outTweenDuration = 1f;
        [SerializeField] private AnimationCurve inCurve;
        [SerializeField] private AnimationCurve outCurve;

        [Header("Message")]
        [SerializeField] private TMP_Text message;

        public override void Init(PPRPopupData data)
        {
            base.Init(data);

            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, inTweenDuration).SetEase(inCurve);
        }

        protected override void OnOpenPopup()
        {
            message.SetText((string) popupData.GenericData);
            base.OnOpenPopup();
        }

        protected override void OnClosePopup()
        {
            transform.DOScale(Vector3.zero,outTweenDuration).SetEase(outCurve).OnComplete(() =>
            {
                base.OnClosePopup();
            });
        }
    }
}