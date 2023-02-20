using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRCurrencyPickupComponent : PPRPickupComponent 
    {
        [SerializeField] public int Amount = 0;
        [SerializeField] public CurrencyTags Currency = CurrencyTags.NA;

        public override void OnReturnedToPool()
        {
            base.OnReturnedToPool();
        }

        public override void OnTakenFromPool()
        {
            base.OnTakenFromPool();
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