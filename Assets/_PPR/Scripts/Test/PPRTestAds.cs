using UnityEngine;
using PPR.Core;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PPR.Game;

namespace PPR.Test
{
    public class PPRTestAds : PPRMonoBehaviour
    {
        [SerializeField] private float adDelay = 5f;
        [SerializeField] private int adResourceLimit = 50;

        private int adResourceCounter = 0;

        private void Start()
        {
            WaitShowAd(adDelay);
            AddListener(PPREvents.currency_collected, CheckResourceAd);
        }

        private async void WaitShowAd(float delay)
        {
            await Task.Delay(delay.SecToMilli());
            PPRManager.Instance.AdManager.ShowAdStandard(null);
        }

        private void CheckResourceAd(object obj)
        {
            var data = (Dictionary<CurrencyTags, int>)obj;

            foreach (var currency in data)
            {
                adResourceCounter += currency.Value;
            }

            if (adResourceCounter >= adResourceLimit)
            {
                adResourceCounter = 0;
                adResourceLimit += 5;
                PPRManager.Instance.AdManager.ShowAdStandard(null);
            }
        }

        private void OnDestroy()
        {
            RemoveListener(PPREvents.currency_collected, CheckResourceAd);
        }
    }
}