using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using System.Threading.Tasks;

namespace PPR.Core
{
    public class PPRConfigManager
    {
        public Action OnInit;

        public PPRConfigManager(Action onComplete)
        {
            PPRDebug.Log($"PPRConfigManager");

            OnInit = onComplete;

            var defaults = new Dictionary<string, object>();
            defaults.Add("upgrade_config", "{}");

            PPRDebug.Log("PPRConfigManager");
            FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults).ContinueWithOnMainThread(OnDefaultValuesSet);
        }

        private void OnDefaultValuesSet(Task task)
        {
            PPRDebug.Log("OnDefaultValuesSet");

            FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero).ContinueWithOnMainThread(OnFetchComplete);
        }

        private void OnFetchComplete(Task obj)
        {
            PPRDebug.Log("OnFetchComplete");

            FirebaseRemoteConfig.DefaultInstance.ActivateAsync().ContinueWithOnMainThread(task => OnActivateComplete(task));
        }

        private void OnActivateComplete(Task obj)
        {
            PPRDebug.Log("OnActivateComplete");
            OnInit.Invoke();
        }

        public async void GetConfigAsync<T>(string configID, Action<T> onComplete)
        {
            PPRDebug.Log($"GetConfigAsync {configID}");

            string saveJson = null;

            await Task.Run(() =>
            {
                saveJson = FirebaseRemoteConfig.DefaultInstance.GetValue(configID).StringValue;
            });

            var saveData = JsonConvert.DeserializeObject<T>(saveJson);

            onComplete.Invoke(saveData);
        }

        public void GetConfigOfflineAsync<T>(string configID, Action<T> onComplete)
        {
            var path = $"Assets/_PPR/Config/{configID}.json";

            var saveJson = File.ReadAllText(path);
            var saveData = JsonConvert.DeserializeObject<T>(saveJson);

            onComplete.Invoke(saveData);
        }
    }
}