using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    [RequireComponent(typeof(Collider2D))]
    public class PPRPickupComponent : PPRTweenInOutComponent
    {
        public bool isCollected = false;

        /// <summary>
        /// Method <c>OnTakenFromPool</c> is called when a poolable object is retrieved from the pool. 
        /// </summary>
        public override void OnTakenFromPool()
        {
            base.OnTakenFromPool();
            Manager.EventManager.InvokeEvent(PPREvents.pickup_taken_from_pool, this);
        }


        /// <summary>
        /// Method <c>OnReturnedToPool</c> is called when a poolable object is returned to the pool.
        /// It rsets the object position
        /// </summary>
        public override void OnReturnedToPool()
        {
            transform.position = Vector3.zero;
            base.OnReturnedToPool();
        }

        protected virtual void OnPickupCollected(object obj)
        {
            Manager.EventManager.InvokeEvent(PPREvents.pickup_collected, this);
            Manager.PoolManager.ReturnPoolable(this);
        }

        protected virtual void OnPickupDestroyed(object obj)
        {
            Manager.EventManager.InvokeEvent(PPREvents.pickup_destroyed, this);
            Manager.PoolManager.ReturnPoolable(this);
        }

        public virtual void CollectPickup()
        {
            OnPickupCollected(this);
        }

        public void DestroyPickup()
        {
            OnPickupDestroyed(this);
        }
    }
}