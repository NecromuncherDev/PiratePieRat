using PPR.Core;
using System.Threading.Tasks;

namespace PPR.Test
{
    public class PPRTestCurrencyUpdater : PPRTestGameStartListener
    {
        protected override async void OnGameStart(object obj)
        {
            await UpdateCurrencyEvents();
        }

        private async Task UpdateCurrencyEvents()
        {
            for (int times = 0; times < 5; times++)
            {
                InvokeEvent(PPRCoreEvents.crew_owned_changed, times);
                InvokeEvent(PPRCoreEvents.pies_owned_changed, times);
                await Task.Delay(500);
            }
        }
    }
}