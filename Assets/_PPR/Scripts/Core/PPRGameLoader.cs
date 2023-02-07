using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace PPR.Core
{
    public class PPRGameLoader : PPRMonoBehaviour
    {
        public void LoadScene(int sceneID) // Called by a button click in the scene, or any other system
        {
            new PPRManager();
            LoadSceneAsync(sceneID);
        }

        private async void LoadSceneAsync(int sceneID)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

            float progressValue;
            while (!operation.isDone)
            {
                progressValue = operation.progress; // Needed for addition of loading bar
                await Task.Yield();
            }

            InvokeEvent(PPRCoreEvents.game_start_event, null);
        }
    }
}

