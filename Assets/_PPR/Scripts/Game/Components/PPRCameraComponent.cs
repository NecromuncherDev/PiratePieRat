using DG.Tweening;
using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRCameraComponent : PPRLogicMonoBehaviour
    {
        [Header("Triggers")]
        [SerializeField] private CurrencyTags triggerCurrency;

        [Header("Shake Parameters")]
        [SerializeField] private float shakeDuration = 0.1f;
        [SerializeField] private float baseStrengthShake = 1f;
        [SerializeField] private int shakeVibBase = 1;

        private void OnEnable()
        {
            AddListener(PPREvents.currency_set, OnCurrencySet);
        }

        private void OnDisable()
        {
            RemoveListener(PPREvents.currency_set, OnCurrencySet);
        }

        private void OnCurrencySet(object obj)
        {
            var scoreEventData = ((CurrencyTags, int))obj;

            if (scoreEventData.Item1 == triggerCurrency)
            {
                ShakeCamera();
            }
        }

        private void ShakeCamera()
        {
            transform.DOShakePosition(shakeDuration, baseStrengthShake, shakeVibBase);
        }
    }
}