using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace PPR.Core
{
    public class PPRLoader : PPRMonoBehaviour
    {
        [SerializeField] private int gameSceneID;
        [SerializeField] private PPRLoaderBase gameLogicLoader;
        [SerializeField] private Canvas popupCanvas;

        private PPRManager manager;

        protected void Start()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(popupCanvas.gameObject);

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

                    var welcomePopup = new PPRPopupData
                    {
                        Canvas = popupCanvas,
                        Priority = 0,
                        PopupType = PopupTypes.WelcomePopup,
                        GenericData = "<b>Welcome Sailor!</b>"
                    };

                    manager.PopupManager.AddPopupToQueue(welcomePopup);

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

