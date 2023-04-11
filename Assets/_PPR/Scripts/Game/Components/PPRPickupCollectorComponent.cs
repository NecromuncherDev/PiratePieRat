using UnityEngine;

namespace PPR.Game
{
    [RequireComponent(typeof(Collider2D))]
    public class PPRPickupCollectorComponent : PPRLogicMonoBehaviour
    {
        [SerializeField] private LayerMask pickupLayer;


        //private void OnCollisionEnter2D(Collision2D col)
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
    }
}