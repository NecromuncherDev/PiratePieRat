using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRUpgradeIntervalComponent : PPRLogicMonoBehaviour
    {
        [SerializeField] private int currencyUpAmount;
        [SerializeField] private CurrencyTags currencyTag;
        [SerializeField] private int intervalTime;

        private void OnEnable()
        {
            Manager.TimerManager.SubscribeTimer(intervalTime, ChangeScore);
        }

        private void OnDisable()
        {
            Manager.TimerManager.UnSubscribeTimer(intervalTime, ChangeScore);
        }

        private void ChangeScore()
        {
            GameLogic.CurrencyManager.ChangeCurrencyByTagByAmount(currencyTag, currencyUpAmount);

            var scoreText = (PPRTweenScoreComponent)Manager.PoolManager.GetPoolable(PoolNames.ScoreToast);
            scoreText.transform.position = transform.position;
            scoreText.Init(currencyUpAmount, null);
        }
    }
}