using PPR.Core;
using PPR.Game;
using UnityEngine;
using System.Collections.Generic;

namespace PPR.Test
{
    public class PPRTestCurrencyUpdater : PPRTestGameStartListener
    {
        [SerializeField] private int startingMetal = 1;
        [SerializeField] private int startingPlastic = 2;
        [SerializeField] private int startingWood = 3;
        private Dictionary<CurrencyTags, int> startingCurrencies = new();

        protected override void OnGameStart(object obj)
        {
            startingCurrencies.Add(CurrencyTags.Metal, startingMetal);
            startingCurrencies.Add(CurrencyTags.Plastic, startingPlastic);
            startingCurrencies.Add(CurrencyTags.Wood, startingWood);

            InvokeEvent(PPREvents.currency_collected, startingCurrencies);
            
            InvokeEvent(PPREvents.currency_pies_per_second_set, 0);
            base.OnGameStart(obj);
        }
    }
}