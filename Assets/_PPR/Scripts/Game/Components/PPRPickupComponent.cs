﻿using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRPickupComponent : PPRPoolable
    {
        private void OnMouseDown()
        {
            Manager.PoolManager.DestroyPool(poolName);
        }

        public override void OnReturnedToPool()
        {
            transform.position = Vector3.zero;
            Manager.EventManager.RemoveListener(PPREvents.stranded_object_taken, OnStrandedTaken);
            base.OnReturnedToPool();
        }

        public override void OnTakenFromPool()
        {
            Manager.EventManager.AddListener(PPREvents.stranded_object_taken, OnStrandedTaken);
            base.OnTakenFromPool();

            Manager.EventManager.InvokeEvent(PPREvents.stranded_object_taken, this);
        }

        private void OnStrandedTaken(object obj)
        {
            Manager.EventManager.RemoveListener(PPREvents.stranded_object_taken, OnStrandedTaken);
            transform.position = Random.insideUnitCircle;
        }
    }
}