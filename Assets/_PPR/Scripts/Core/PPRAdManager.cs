using System;
using UnityEngine.Advertisements;

namespace PPR.Core
{
    public class PPRAdManager : IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private string _adStandardId = "Interstitial_Android";
        private string _adRewardedId = "Rewarded_Android";

        private bool isAdLoaded = false;
        private Action<bool> onShowStandardComplete;
        private Action<bool> onShowRewardedComplete;

        public PPRAdManager()
        {
            Advertisement.Initialize("5215653");
            LoadAd();
        }

        #region Load Ad
        public void LoadAd()
        {
            Advertisement.Load(_adStandardId, this);
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            isAdLoaded = true;
            PPRDebug.Log("OnUnityAdsAdLoaded");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            isAdLoaded = false;
            PPRManager.Instance.CrashManager.LogExceptionHandling(message);
        }
        #endregion

        #region Show Ad
        public void ShowAdStandard(Action<bool> onShowStandardAdComplete)
        {
            if (isAdLoaded)
            {
                onShowStandardAdComplete?.Invoke(false);
                return;
            }

            onShowStandardComplete = onShowStandardAdComplete;
            Advertisement.Show(_adStandardId, this);
        }

        public void ShowAdRewarded(Action<bool> onShowRewardedAdComplete)
        {
            if (isAdLoaded)
            {
                onShowRewardedAdComplete?.Invoke(false);
                return;
            }

            onShowRewardedComplete = onShowRewardedAdComplete;
            Advertisement.Show(_adRewardedId);
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            PPRManager.Instance.AnalyticsManager.ReportEvent(PPREvents.ad_show_start);
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            PPRManager.Instance.AnalyticsManager.ReportEvent(PPREvents.ad_show_click);
            onShowStandardComplete?.Invoke(true);
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            PPRManager.Instance.AnalyticsManager.ReportEvent(PPREvents.ad_show_complete);
            onShowStandardComplete?.Invoke(true);

            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED && placementId == _adRewardedId)
            {
                onShowRewardedComplete?.Invoke(true);
            }

            onShowStandardComplete = null;
            onShowRewardedComplete = null;
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            onShowStandardComplete?.Invoke(false);
            onShowRewardedComplete?.Invoke(false);

            onShowStandardComplete = null;
            onShowRewardedComplete = null;
        }
        #endregion
    }
}