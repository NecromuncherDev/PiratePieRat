using PPR.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PPR.Game
{
    public class PPRCurrencyView : PPRMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyLabel;
        [SerializeField] private PPREvents currencyUpdateEnvent;

        private void OnEnable()
        {
            AddListener(currencyUpdateEnvent, UpdateView);
        }

        private void OnDisable()
        {
            RemoveListener(currencyUpdateEnvent, UpdateView);
        }

        private void UpdateView(object obj)
        {
            SetAmount((int)obj);
        }

        internal void SetAmount(int newAmount)
        {
            currencyLabel.text = $"{newAmount}";
        }
    }
}
