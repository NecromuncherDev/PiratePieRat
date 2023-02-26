using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRMainUIComponent : PPRLogicMonoBehaviour
    {

        private void OnEnable()
        {
            AddListener(PPREvents.currency_collected, OnCurrencySet);
        }

        private void OnDisable()
        {
            RemoveListener(PPREvents.currency_collected, OnCurrencySet);
        }

        private void OnCurrencySet(object obj)
        {
            var scoreEventData = ((CurrencyTags, int))obj;

            if (scoreEventData.Item1 == CurrencyTags.Pies)
            {
                GeneratePopup(scoreEventData.Item2);
                //scoreText.text = scoreEventData.Item2.ToString("N0");
            }
        }

        private void GeneratePopup(int amount)
        {
            if (amount <= 0)
                return;

            //var power = GameLogic.UpgradeManager.GetPowerByIDAndLevel(clickUpgradeData.UpgradeableTypeID, clickUpgradeData.CurrentLevel);

            //GameLogic.CurrencyManager.ChangeCurrencyByTagByAmount(currencyTag, power);

            var scoreText = (PPRTweenScoreComponent)Manager.PoolManager.GetPoolable(PoolNames.ScoreToast);
            Vector3 spawnPoint = new Vector3(Screen.width / 2, Screen.height / 2, -Camera.main.transform.position.z);
            scoreText.transform.position = Camera.main.ScreenToWorldPoint(spawnPoint);
            scoreText.Init(amount);
        }

        public void OnUpgradePressed()
        {
            GameLogic.UpgradeManager.UpgradeItemByID(UpgradeableTypeIDs.ClickPowerUpgrade);
        }
    }
}