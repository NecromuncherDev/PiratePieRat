using PPR.Core;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PPR.Game
{
    public class PPRCurrencyPickupComponent : PPRPickupComponent 
    {
        [SerializeField] private int amountMin = 0;
        [SerializeField] private int amountMax = 0;
        [SerializeField] private List<CurrencyTags> currencies = new();
        public Dictionary<CurrencyTags, int> Payload = new();


        public override void OnReturnedToPool()
        {
            base.OnReturnedToPool();
        }

        public override void OnTakenFromPool()
        {
            InitPayload();
            base.OnTakenFromPool();
        }

        private void InitPayload()
        {
            Payload = new();
            foreach (var currency in currencies)
            {
                Payload[currency] = Random.Range(amountMin, amountMax);
            }
        }

        protected override void OnPickupCollected(object obj)
        {
            Manager.EventManager.InvokeEvent(PPREvents.pickup_collected, this);
            Manager.PoolManager.ReturnPoolable(this);
        }

        protected override void OnPickupDestroyed(object obj)
        {
            Manager.EventManager.InvokeEvent(PPREvents.pickup_destroyed, this);
            Manager.PoolManager.ReturnPoolable(this);
        }
    }
}