using PPR.Core;
using PPR.Game;

namespace PPR.Test
{
    public class PPRTestCurrencyUpdater : PPRTestGameStartListener
    {
        protected override void OnGameStart(object obj)
        {
            InvokeEvent(PPREvents.currency_pies_per_second_set, 0);
            InvokeEvent(PPREvents.currency_collected, (CurrencyTags.Crew, 0));
            InvokeEvent(PPREvents.currency_collected, (CurrencyTags.Pies, 0));
            base.OnGameStart(obj);
        }
    }
}