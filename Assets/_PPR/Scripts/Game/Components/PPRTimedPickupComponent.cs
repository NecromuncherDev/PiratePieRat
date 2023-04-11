using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRTimedPickupComponent : PPRPickupComponent
    {
        protected float gatherDuration;
        
        private bool isGatherCanceled;
        private int alarmID;
        private PPRPickupTimerComponent pickupTimer;
        //private PPRPoolable timerPopup;

        public override void CollectPickup()
        {
            isGatherCanceled = false;
            alarmID = Manager.TimerManager.SetAlarm(gatherDuration, VerifyCollect);
            Debug.Log($"Waiting {gatherDuration} seconds to pickup...");

            OpenTimer();
        }
        private void OpenTimer()
        {
            // Create popup with timer using pool
            pickupTimer = (PPRPickupTimerComponent)Manager.PoolManager.GetPoolable(PoolNames.TimerPickup);
            pickupTimer.Init(gatherDuration);
            pickupTimer.transform.position = transform.position;
        }

        private void VerifyCollect()
        {
            if (!isGatherCanceled)
            {
                isCollected = true;
                base.CollectPickup();
            }
        }

        public void CancelPickup()
        {
            if (!isCollected)
            {
                isGatherCanceled = true;
                Manager.TimerManager.DisableAlarm(alarmID);
            }

            CloseTimer();
        }

        private void CloseTimer()
        {
            Manager.PoolManager.ReturnPoolable(pickupTimer);
            pickupTimer = null;
        }
    }
}