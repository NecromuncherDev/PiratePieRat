#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace PPR.Util
{
	public class ClearDataTool
    {
        [MenuItem("Tools/Clear Data Tool")]
        public static void ClearAllDataTool()
        {
            var path = Application.persistentDataPath;
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                Debug.Log(file);
                if (file.Contains("PPR"))
                {
                    File.Delete(file);
                }
            }

            PlayerPrefs.DeleteAll();
        }
    }
}
#endif