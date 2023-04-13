using PPR.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;


namespace PPR.Game
{
    public class PPRMainUIComponent : PPRLogicMonoBehaviour
    {
        [SerializeField] private List<CurrencyImage> currencyImages = new();

        [Serializable]
        public struct CurrencyImage
        {
            [SerializeField] public CurrencyTags tag;
            [SerializeField] public Sprite image;
        }

        private void OnEnable()
        {
            AddListener(PPREvents.currency_collected, OnCurrencySet);
        }

        private void OnDisable()
        {
            RemoveListener(PPREvents.currency_collected, OnCurrencySet);
        }

        private async void OnCurrencySet(object obj)
        {
            var scoreEventData = (Dictionary<CurrencyTags, int>)obj;

            foreach (var currency in scoreEventData)
            {
                GeneratePopup(currency.Key, currency.Value);
                await Task.Delay(300);
            }
        }

        private void GeneratePopup(CurrencyTags tag, int amount)
        {
            if (tag == CurrencyTags.NA || amount <= 0)
                return;

            var scoreText = (PPRTweenScoreComponent)Manager.PoolManager.GetPoolable(PoolNames.ScoreToast);
            Vector3 spawnPoint = new Vector3(Screen.width / 2, Screen.height / 2, -Camera.main.transform.position.z);
            scoreText.transform.position = Camera.main.ScreenToWorldPoint(spawnPoint);
            scoreText.Init(amount,
                           currencyImages.First(x => x.tag == tag).image,
                           Random.Range(-0.25f, 0.25f));
        }

        public void OnUpgradePressed()
        {
            GameLogic.UpgradeManager.UpgradeItemByID(UpgradeableTypeIDs.RadarRange);
        }
    }
}