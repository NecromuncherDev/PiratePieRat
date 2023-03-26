using UnityEngine;
using PPR.Core;
using System;
using System.Threading.Tasks;

namespace PPR.Test
{
    public class PPRTestAds : PPRMonoBehaviour
    {
        [SerializeField] private float adDelay = 5f;

        private void Start()
        {
            WaitShowAd(adDelay);
        }

        private async void WaitShowAd(float delay)
        {
            await Task.Delay((int)(delay * 1000));
            PPRManager.Instance.AdManager.ShowAdStandard(null);
        }
    }
}