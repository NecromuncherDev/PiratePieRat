using PPR.Core;
using System.Threading;
using System.Threading.Tasks;

namespace PPR.Game
{
    public class PPRRatBasedPieGenerator
    {
        /// 1. Add listeners to
        ///     1.1. Whenever Rat is added or lost
        ///     1.2. Whenever upgradeableRat is upgraded
        /// 2. Whenever there's anything rat related (step 1)
        ///     2.1. Update PiesPerSecond accordingly
        ///     2.2. InvokeEvent(PPREvents.currency_pies_per_second_set, piesPerSecond)
        /// 3. Each second add pies
        ///     3.1. InvokeEvent(PPREvents.currency_collected, (CurrencyTags.pies, piesPerSecond))

        private int piesPerSecond = 0; // TODO: Load this from save
        private int crewCount = 0;
        private int ratPower = 0;

        private bool isGenerating = false;

        public PPRRatBasedPieGenerator()
        {
            PPRManager.Instance.EventManager.AddListener(PPREvents.game_start_event, StartGeneratePies); // Could be loading based bug
            PPRManager.Instance.EventManager.AddListener(PPREvents.game_stop_event, StopGeneratePies);

            PPRManager.Instance.EventManager.AddListener(PPREvents.currency_crew_set, UpdatePiesPerSecondByCrew);
            PPRManager.Instance.EventManager.AddListener(PPREvents.item_upgraded, UpdatePiesPerSecondByPower);

            ratPower = GetRatPowerFromUpgradableConfig(); // Could be loading based bug
        }

        private int GetRatPowerFromUpgradableConfig()
        {
            var upgradeableRatData = PPRGameLogic.Instance.UpgradeManager.GetUpgradeableByID(UpgradeableTypeIDs.RatPowerUpgrade);
            int power = PPRGameLogic.Instance.UpgradeManager.GetPowerByIDAndLevel(UpgradeableTypeIDs.RatPowerUpgrade, upgradeableRatData.CurrentLevel);

            return power;
        }

        private void UpdatePiesPerSecondByPower(object obj)
        {
            var upgradedType = (UpgradeableTypeIDs)obj;

            if (upgradedType == UpgradeableTypeIDs.RatPowerUpgrade)
            {
                ratPower = GetRatPowerFromUpgradableConfig();
                UpdatePiesPerSecond();
            }
        }

        private void UpdatePiesPerSecondByCrew(object obj)
        {
            var updatedCrew = (int)obj;

            if (crewCount != updatedCrew)
            {
                crewCount = updatedCrew;
                UpdatePiesPerSecond();
            }
        }

        private void UpdatePiesPerSecond()
        {
            piesPerSecond = crewCount * ratPower;
            PPRManager.Instance.EventManager.InvokeEvent(PPREvents.currency_pies_per_second_set, piesPerSecond);
        }

        private async void StartGeneratePies(object obj)
        {
            isGenerating = true;

            while (isGenerating)
            {
                if (piesPerSecond > 0)
                    PPRManager.Instance.EventManager.InvokeEvent(PPREvents.currency_collected, (CurrencyTags.Pies, piesPerSecond));

                await Task.Delay(1000); // 1 second
            }

            PPRDebug.Log("Done generating pies");
        }

        private void StopGeneratePies(object obj)
        {
            isGenerating = false;
        }

        ~PPRRatBasedPieGenerator()
        {
            PPRManager.Instance.EventManager.RemoveListener(PPREvents.game_start_event, StartGeneratePies);
            PPRManager.Instance.EventManager.RemoveListener(PPREvents.game_stop_event, StopGeneratePies);

            PPRManager.Instance.EventManager.RemoveListener(PPREvents.currency_crew_set, UpdatePiesPerSecondByCrew);
            PPRManager.Instance.EventManager.RemoveListener(PPREvents.item_upgraded, UpdatePiesPerSecondByPower);
        }
    }
}