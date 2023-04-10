using PPR.Core;
using System.Collections.Generic;
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
            var scoreEventData = (Dictionary<CurrencyTags, int>)obj;

            if (scoreEventData.ContainsKey(CurrencyTags.Plastic))
            {
                GeneratePopup(scoreEventData[CurrencyTags.Plastic]);
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
            GameLogic.UpgradeManager.UpgradeItemByID(UpgradeableTypeIDs.RatPowerUpgrade);
        }
    }
}