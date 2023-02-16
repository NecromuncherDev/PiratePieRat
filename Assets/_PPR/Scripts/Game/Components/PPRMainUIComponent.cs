using PPR.Core;
using TMPro;
using UnityEngine;

namespace PPR.Game
{
    public class PPRMainUIComponent : PPRLogicMonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        private void OnEnable()
        {
            var score = 0;
            GameLogic.CurrencyManager.TryGetCurrencyByTag(CurrencyTags.Pies, ref score);
            scoreText.text = score.ToString("N0");

            AddListener(PPREvents.currency_set, OnCurrencySet);
        }

        private void OnDisable()
        {
            RemoveListener(PPREvents.currency_set, OnCurrencySet);
        }

        private void OnCurrencySet(object obj)
        {
            var scoreEventData = ((CurrencyTags, int))obj;

            if (scoreEventData.Item1 == CurrencyTags.Pies)
            {
                scoreText.text = scoreEventData.Item2.ToString("N0");
            }
        }

        public void OnUpgradePressed()
        {
            //GameLogic.UpgradeManager.UpgradeItemByID(UpgradeableTypeIDs.ClickPowerUpgrade);
        }
    }
}