using PPR.Core;

namespace PPR.Game
{
    public class PPRPickupManager : PPRLogicMonoBehaviour
    {
        private void OnEnable()
        {
            AddListener(PPREvents.pickup_collected, CollectCurrencyFromPickup);
        }

        private void CollectCurrencyFromPickup(object obj)
        {
            var pickup = (PPRCurrencyPickupComponent)obj;
            InvokeEvent(PPREvents.currency_collected, (pickup.Currency, pickup.Amount));
        }

        private void OnDisable()
        {
            RemoveListener(PPREvents.pickup_collected, CollectCurrencyFromPickup);
        }
    }
}