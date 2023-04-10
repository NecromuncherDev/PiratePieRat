using PPR.Core;
using UnityEngine;
using System.Collections.Generic;

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
            var currencyEventData = (Dictionary<CurrencyTags, int>)obj;

            foreach (var currency in currencyEventData)
            {
                switch (currency.Key)
                {
                    case CurrencyTags.Metal:
                        InvokeEvent(PPREvents.currency_metal_set, currency.Value);
                        break;
                    case CurrencyTags.Plastic:
                        InvokeEvent(PPREvents.currency_plastic_set, currency.Value);
                        break;
                    case CurrencyTags.Wood:
                        InvokeEvent(PPREvents.currency_wood_set, currency.Value);
                        break;
                    default:
                        Debug.LogError($"PPRHeadsUpDisplay - Unknown currency updated: {currency.Key}");
                        break;
                }
            }

            
        }

        private void OnDisable()
        {
            RemoveListener(PPREvents.currency_set, OnCurrencySet);
        }
    }
}
