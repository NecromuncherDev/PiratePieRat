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
            var data = ((CurrencyTags, int))obj;

            switch (data.Item1)
            {
                case CurrencyTags.Metal:
                    InvokeEvent(PPREvents.currency_metal_set, data.Item2);
                    break;
                case CurrencyTags.Plastic:
                    InvokeEvent(PPREvents.currency_plastic_set, data.Item2);
                    break;
                case CurrencyTags.Wood:
                    InvokeEvent(PPREvents.currency_wood_set, data.Item2);
                    break;
                default:
                    Debug.LogError($"PPRHeadsUpDisplay - Unknown currency updated: {data.Item1}");
                    break;
            }
        }
        private void OnDisable()
        {
            RemoveListener(PPREvents.currency_set, OnCurrencySet);
        }
    }
}
