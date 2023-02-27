using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace PPR.Core
{

    public class PPRSaveManager
    {
        private const string SAVE_FILE_EXTENTION = "pprSave";

        public void Save(IPPRSaveData saveData)
        {
            var saveID = saveData.GetType().FullName;
            Debug.Log(saveID);
            var saveJson = JsonConvert.SerializeObject(saveData);
            Debug.Log(saveJson);

            var path = $"{Application.persistentDataPath}/{saveID}.{SAVE_FILE_EXTENTION}";

            File.WriteAllText(path, saveJson);
        }

        public void Load<T>(Action<T> onComplete) where T : IPPRSaveData
        {
            if (!HasData<T>())
            {
                onComplete.Invoke(default);
                return;
            }

            var saveID = typeof(T).FullName;
            var path = $"{Application.persistentDataPath}/{saveID}.{SAVE_FILE_EXTENTION}";

            var saveJson = File.ReadAllText(path);
            var saveData = JsonConvert.DeserializeObject<T>(saveJson);

            Debug.Log(saveID);
            Debug.Log(saveJson);

            onComplete.Invoke(saveData);

        }

        public bool HasData<T>() where T : IPPRSaveData
        {
            var saveID = typeof(T).FullName;
            var path = $"{Application.persistentDataPath}/{saveID}.hogSave";
            return File.Exists(path);
        }
    }

    public interface IPPRSaveData
    {
    }
}
