using PPR.Core;

namespace PPR.Test
{
    public class PPRTestGameStartListener : PPRMonoBehaviour
    {
        private void Start()
        {
            AddListener(PPREvents.game_start_event, OnGameStart);
        }

        private void OnDestroy()
        {
            RemoveListener(PPREvents.game_start_event, OnGameStart);
        }

        protected virtual void OnGameStart(object obj)
        {
            print("PPRTester - OnGameStart");
            //Destroy(gameObject);
        }
    }
}