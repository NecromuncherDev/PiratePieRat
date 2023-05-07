using System;
using UnityEngine;

namespace PPR.Game
{
    [RequireComponent(typeof(Collider2D))]
    public class PPRPickupCollectorComponent : PPRLogicMonoBehaviour
    {
        [SerializeField] private LayerMask pickupLayer;
        [SerializeField] private CircleCollider2D gatheringCollider;
        float pickupRadiusMultiplier;
        float pickupRadiusOriginal;

        private void OnEnable()
        {
            AddListener(Core.PPREvents.upgraded_gathering_range, SetGatheringRadiusMultiplier);
        }

        private void Start()
        {
            pickupRadiusOriginal = gatheringCollider.radius;
            SetGatheringRadiusMultiplier();
        }

        private void SetGatheringRadiusMultiplier(object obj = null)
        {
            pickupRadiusMultiplier = GameLogic.UpgradeManager.GetCurrentValueByID(UpgradeableTypeIDs.GatheringRange);
            gatheringCollider.radius = pickupRadiusOriginal * pickupRadiusMultiplier;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            //if ((pickupLayer.value & (1 << col.transform.gameObject.layer)) > 0)
            //{
            if (col.gameObject.TryGetComponent(out PPRPickupComponent pickup))
            {
                pickup.CollectPickup();
            }
            //}
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out PPRTimedPickupComponent pickup))
            {
                pickup.CancelPickup();
            }
        }

        private void OnDisable()
        {
            RemoveListener(Core.PPREvents.upgraded_gathering_range, SetGatheringRadiusMultiplier);
        }
    }
}