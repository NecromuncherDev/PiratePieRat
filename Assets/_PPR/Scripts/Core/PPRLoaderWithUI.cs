using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace PPR.Core
{
    public class PPRLoaderWithUI : PPRLoader
    {
        [SerializeField] private GameObject loadingScrene;
        [SerializeField] private Image loadingBarFill;

        protected override async Task WaitForSceneLoad(AsyncOperation operation)
        {
            ShowUI();

            float progressValue;
            while (!operation.isDone)
            {
                progressValue = operation.progress;
                loadingBarFill.fillAmount = progressValue;
                await Task.Yield();
            }
        }

        private void ShowUI()
        {
            loadingScrene.SetActive(true);
        }
    }
}