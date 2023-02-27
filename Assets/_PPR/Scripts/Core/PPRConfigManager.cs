using Newtonsoft.Json;
using System;
using System.IO;

namespace PPR.Core
{
    public class PPRConfigManager
    {
        public void GetConfigAsync<T>(string configID, Action<T> onComplete)
        {
            var path = $"Assets/_PPR/Config/{configID}.json";

            var saveJson = File.ReadAllText(path);
            var saveData = JsonConvert.DeserializeObject<T>(saveJson);

            onComplete.Invoke(saveData);
        }
    }
}
