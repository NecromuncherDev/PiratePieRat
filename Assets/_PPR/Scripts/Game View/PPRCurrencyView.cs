using PPR.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PPR.GameView
{
    public class PPRCurrencyView : PPRMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyLabel;

        internal void SetAmount(int newAmount)
        {
            currencyLabel.text = $"{newAmount}";
        }
    }
}
