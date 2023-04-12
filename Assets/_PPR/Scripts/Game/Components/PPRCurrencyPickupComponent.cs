using DG.Tweening;
using PPR.Core;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PPR.Game
{
    public class PPRCurrencyPickupComponent : PPRTimedPickupComponent 
    {
        [Header("Currency")]
        [SerializeField] private int amountMin = 0;
        [SerializeField] private int amountMax = 0;
        [SerializeField] private List<CurrencyTags> currencies = new();
        public Dictionary<CurrencyTags, int> Payload = new();
        private int totalPayloadSum = 0;

        public override void OnReturnedToPool()
        {
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(base.OnReturnedToPool);
        }

        public override void OnTakenFromPool()
        {
            InitPayload();
            gatherDuration = totalPayloadSum; // TODO: Change to be configurable based on ship stats

            transform.localScale = scaleStart * Vector3.one;
            transform.DOScale(scaleEnd * Vector3.one, tweenTimeIn)
                .SetEase(inCurve)
                .OnComplete(() => base.OnTakenFromPool());
        }

        private void InitPayload()
        {
            totalPayloadSum = 0;
            Payload = new();
            foreach (var currency in currencies)
            {
                Payload[currency] = Random.Range(amountMin, amountMax);
                totalPayloadSum += Payload[currency];
            }
        }

        protected override void OnPickupCollected(object obj)
        {
            Manager.EventManager.InvokeEvent(PPREvents.pickup_collected, this);

            transform.DOScale(scaleStart * Vector3.one, tweenTimeOut)
                .SetEase(outCurve)
                .OnComplete(() => Manager.PoolManager.ReturnPoolable(this));
        }

        protected override void OnPickupDestroyed(object obj)
        {
            Manager.EventManager.InvokeEvent(PPREvents.pickup_destroyed, this);

            transform.DOScale(scaleStart * Vector3.one, tweenTimeOut)
                .SetEase(outCurve)
                .OnComplete(() => Manager.PoolManager.ReturnPoolable(this));
        }
    }
}