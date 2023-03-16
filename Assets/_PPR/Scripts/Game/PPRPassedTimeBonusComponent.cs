using PPR.Core;

namespace PPR.Game
{
    public class PPRPassedTimeBonusComponent : PPRLogicMonoBehaviour
    {
        private void Start()
        {
            GiveBonusAccordingToTimePassed(Manager.TimerManager.GetLastOfflineTimeSeconds());

            Manager.EventManager.AddListener(PPREvents.offline_time_refreshed, OnRefreshedTime);
        }

        private void OnDestroy()
        {
            Manager.EventManager.RemoveListener(PPREvents.offline_time_refreshed, OnRefreshedTime);
        }

        private void OnRefreshedTime(object timePassed)
        {
            GiveBonusAccordingToTimePassed((int)timePassed);
        }

        // TODO: Change to be configurable
        private void GiveBonusAccordingToTimePassed(int timePassed)
        {
            var returnBonus = timePassed / 180; // Once every 3 minutes
            GameLogic.CurrencyManager.ChangeCurrencyByTagByAmount(CurrencyTags.Pies, returnBonus); // Gaining Pies (specifically)
        }
    }
}