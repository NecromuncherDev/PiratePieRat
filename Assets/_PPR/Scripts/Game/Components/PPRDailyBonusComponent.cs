using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRDailyBonusComponent : PPRLogicMonoBehaviour
    {
        [SerializeField] private int hours = 24;

        private void Start()
        {
            var lastOfflineTimeSeconds = Manager.TimerManager.GetLastOfflineTimeSeconds();
            var leftOverTime = Manager.TimerManager.GetLeftOverTime(OfflineTimeTypes.DailyBonus);

            var totalSeconds = lastOfflineTimeSeconds + leftOverTime;

            if (totalSeconds >= hours.HoursToSeconds())
            {
                GameLogic.CurrencyManager.ChangeCurrencyByTagByAmount(CurrencyTags.Pies, 1000);
                var leftOver = totalSeconds - hours.HoursToSeconds();
                Manager.TimerManager.SetLeftOverTime(OfflineTimeTypes.DailyBonus, Mathf.Min(leftOver / 2, leftOver));
            }
            else
            {
                Manager.TimerManager.SetLeftOverTime(OfflineTimeTypes.DailyBonus, totalSeconds);
            }
        }
    }
}