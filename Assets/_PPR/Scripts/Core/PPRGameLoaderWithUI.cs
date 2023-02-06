using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace PPR.Core
{
    public class PPRGameLoaderWithUI : PPRMonoBehaviour
    {
        [SerializeField] private GameObject loadingScrene;
        [SerializeField] private Image loadingBarFill;

        public void LoadScene(int sceneID)
        {
            new PPRManager();
            LoadSceneAsync(sceneID);
        }

        private async void LoadSceneAsync(int sceneID)
        {
            loadingScrene.gameObject.SetActive(true);
            
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
            
            float progressValue;
            while (!operation.isDone)
            {
                progressValue = operation.progress;
                loadingBarFill.fillAmount = progressValue;

                await Task.Yield();
            }

            InvokeEvent(PPRGameEvents.game_start_event, null);
        }
    }
}