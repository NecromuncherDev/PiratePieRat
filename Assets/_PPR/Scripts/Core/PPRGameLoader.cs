using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace PPR.Core
{
    public class PPRGameLoader : PPRMonoBehaviour
    {
        [SerializeField] private PPRGameLoaderBase gameLogicLoader;

        private PPRManager manager;

        protected void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(int sceneID) // Called by a button click in the scene, or any other system
        {
            manager = new PPRManager();
            LoadSceneAsync(sceneID);
        }

        private void LoadSceneAsync(int sceneID)
        {
            manager.LoadManager(() =>
            {
                gameLogicLoader.StartLoad(async () =>
                {
                    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
                    await WaitForSceneLoad(operation);
                    InvokeEvent(PPREvents.game_start_event, null);
                    Destroy(gameObject);
                });
            });
        }

        protected virtual async Task WaitForSceneLoad(AsyncOperation operation)
        {
            while (!operation.isDone)
            {
                await Task.Yield();
            }
        }
    }
}

