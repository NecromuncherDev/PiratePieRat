using PPR.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PPR.GameView
{
    public class PPRCurrencyView : PPRMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyLabel;
        [SerializeField] private Image currencyImage;

        [Header("TEMPORARY")]
        [SerializeField] private Sprite TEST_currencySprite;

        private void OnEnable() // TODO: Remove once there's a currency manager to change this in code
        {
            Initialize(0, TEST_currencySprite);
        }

        internal void Initialize(int currencyAmount, Sprite currencySprite)
        {
            SetAmount(currencyAmount);
            currencyImage.sprite = currencySprite;
            currencyImage.preserveAspect = true;
        }

        internal void SetAmount(int newAmount)
        {
            currencyLabel.text = $"{newAmount}";
        }
    }
}
