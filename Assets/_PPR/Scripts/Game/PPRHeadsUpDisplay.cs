using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRHeadsUpDisplay : PPRMonoBehaviour
    {
        private void OnEnable()
        {
            AddListener(PPREvents.currency_set, OnCurrencySet);
        }

        private void OnCurrencySet(object obj)
        {
            var currencyEventData = ((CurrencyTags, int))obj;

            switch (currencyEventData.Item1)
            {
                case CurrencyTags.Crew:
                    InvokeEvent(PPREvents.currency_crew_set, currencyEventData.Item2);
                    break;
                case CurrencyTags.Pies:
                    InvokeEvent(PPREvents.currency_pies_set, currencyEventData.Item2);
                    break;
                case CurrencyTags.Cheese:
                    InvokeEvent(PPREvents.currency_cheese_set, currencyEventData.Item2);
                    break;
                default:
                    Debug.LogError($"PPRHeadsUpDisplay - Unknown currency updated: {currencyEventData.Item1}");
                    break;
            }

            
        }

        private void OnDisable()
        {
            RemoveListener(PPREvents.currency_set, OnCurrencySet);
        }
    }
}
