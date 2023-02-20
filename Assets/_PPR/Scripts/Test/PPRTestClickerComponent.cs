using PPR.Core;
using UnityEngine;

namespace PPR.Test
{
    public class PPRTestClickerComponent : PPRMonoBehaviour
    {
        public void OnMouseUpAsButton()
        {
            //TODO: Convert level to power from config
            //GameLogic.CurrencyManager.ChangeCurrencyByTagByAmount(currencyTag, clickUpgradeData.CurrentLevel);

            //var scoreText = (PPRTweenScoreComponent)Manager.PoolManager.GetPoolable(PoolNames.ScoreToast);
            //scoreText.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * -Camera.main.transform.position.z;
            //scoreText.Init(clickUpgradeData.CurrentLevel);

            Debug.Log($"{gameObject.name} has been clicked");
        }
    }
}