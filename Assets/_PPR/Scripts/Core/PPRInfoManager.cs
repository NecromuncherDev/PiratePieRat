using Newtonsoft.Json;
using System;

namespace PPR.Core
{
    public class PPRInfoManager
    {
        public PPRGGameTipsConfig GameTipsConfig;
        private Action initCompleteAction;

        public PPRInfoManager(Action onComplete)
        {
            initCompleteAction = onComplete;
            PPRManager.Instance.ConfigManager.GetConfigAsync<PPRGGameTipsConfig>("tips_config", OnGameTipsLoaded);
        }

        private void OnGameTipsLoaded(PPRGGameTipsConfig tipsConfig)
        {
            GameTipsConfig = tipsConfig;
            initCompleteAction.Invoke();
        }

        public PPRGGameTipsConfig GetTipsConfig()
        {
            return GameTipsConfig;
        }
    }

    [Serializable]
    public class PPRGGameTipsConfig
    {
        [JsonProperty("change_interval")]
        public float ChangeInterval;

        [JsonProperty("game_tips")]
        public string[] GameTips;
    }
}
