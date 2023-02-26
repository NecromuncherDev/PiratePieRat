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
            var power = GameLogic.UpgradeManager.GetPowerByIDAndLevel(clickUpgradeData.UpgradeableTypeID, clickUpgradeData.CurrentLevel);

            GameLogic.CurrencyManager.ChangeCurrencyByTagByAmount(currencyTag, power);

            var scoreText = (PPRTweenScoreComponent)Manager.PoolManager.GetPoolable(PoolNames.ScoreToast);
            scoreText.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * -Camera.main.transform.position.z;
            scoreText.Init(power);
        }
    }
}