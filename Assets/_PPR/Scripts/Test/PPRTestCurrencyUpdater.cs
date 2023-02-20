using PPR.Core;
using PPR.Game;
using System.Threading.Tasks;

namespace PPR.Test
{
    public class PPRTestCurrencyUpdater : PPRTestGameStartListener
    {
        protected override async void OnGameStart(object obj)
        {
            InvokeEvent(PPREvents.currency_collected, (CurrencyTags.Crew, 0));
            InvokeEvent(PPREvents.currency_collected, (CurrencyTags.Pies, 0));
            base.OnGameStart(obj);
        }

        //private async Task UpdateCurrencyEvents()
        //{
        //    int maxTimes = 15;
        //    for (int times = 0; times < maxTimes; times++)
        //    {
        //        InvokeEvent(PPREvents.currency_collected, times + 1);
        //        InvokeEvent(PPREvents.currency_pies_set, times + 1);
        //        await Task.Delay(50);
        //    }
        //}
    }
}