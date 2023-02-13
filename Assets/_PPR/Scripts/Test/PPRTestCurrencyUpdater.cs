using PPR.Core;
using System.Threading.Tasks;

namespace PPR.Test
{
    public class PPRTestCurrencyUpdater : PPRTestGameStartListener
    {
        protected override async void OnGameStart(object obj)
        {
            await UpdateCurrencyEvents();
            base.OnGameStart(obj);
        }

        private async Task UpdateCurrencyEvents()
        {
            int maxTimes = 15;
            for (int times = 0; times < maxTimes; times++)
            {
                InvokeEvent(PPREvents.crew_owned_changed, times + 1);
                InvokeEvent(PPREvents.pies_owned_changed, times + 1);
                await Task.Delay(50);
            }
        }
    }
}