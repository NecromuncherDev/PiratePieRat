using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace PPR.Core
{
    public class PPRLoader : PPRMonoBehaviour
    {
        [SerializeField] private int gameSceneID;
        [SerializeField] private PPRLoaderBase gameLogicLoader;

        private PPRManager manager;

        protected void Start()
        {
            DontDestroyOnLoad(gameObject);
            // DontDestroyOnLoad(popupCanvas.gameObject);

            WaitForSeconds(2, () => LoadScene(gameSceneID));
            //LoadScene(gameSceneID);
        }

        public void LoadScene(int sceneID)
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
                    InvokeEvent(PPREvents.game_start_event);
                    
                    ShowWelcomeMessage(PPRPopupData.WelcomeMessage);

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

        public void ShowWelcomeMessage(PPRPopupData data)
        {
            manager.PopupManager.AddPopupToQueue(data);
        }
    }
}

