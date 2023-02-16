using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRMainClickerComponent : PPRLogicMonoBehaviour
    {
        [SerializeField] private PPRPoolConfiguration scorePoolConfig;
        [SerializeField] private CurrencyTags currencyTag;

        PPRUpgradeableData clickUpgradeData;
        int score = 0;

        protected override void Awake()
        {
            base.Awake();
            Manager.PoolManager.InitPool(scorePoolConfig);
            clickUpgradeData = GameLogic.UpgradeManager.GetUpgradeableByID(UpgradeableTypeIDs.ClickPowerUpgrade);
        }

        public void OnMouseUpAsButton()
        {
            //TODO: Convert level to power from config
            GameLogic.CurrencyManager.ChangeCurrencyByTagByAmount(currencyTag, clickUpgradeData.CurrentLevel);

            var scoreText = (PPRTweenScoreComponent)Manager.PoolManager.GetPoolable(PoolNames.ScoreToast);
            scoreText.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * -Camera.main.transform.position.z;
            scoreText.Init(clickUpgradeData.CurrentLevel);
        }
    }
}