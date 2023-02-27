using PPR.Core;
using PPR.Game;
using UnityEngine;

namespace PPR.Test
{
    public class PPRTestCurrencyUpdater : PPRTestGameStartListener
    {
        [SerializeField] private int startingCrew = 1;
        [SerializeField] private int startingPies = 0;

        protected override void OnGameStart(object obj)
        {
            InvokeEvent(PPREvents.currency_pies_per_second_set, 0);
            InvokeEvent(PPREvents.currency_collected, (CurrencyTags.Pies, startingPies));
            InvokeEvent(PPREvents.currency_collected, (CurrencyTags.Crew, startingCrew));
            base.OnGameStart(obj);
        }
    }
}