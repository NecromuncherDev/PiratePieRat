using PPR.Core;

namespace PPR.Test
{
    public class PPRTestGameStartListener : PPRMonoBehaviour
    {
        private void Start()
        {
            AddListener(PPRCoreEvents.game_start_event, OnGameStart);
        }

        private void OnDestroy()
        {
            RemoveListener(PPRCoreEvents.game_start_event, OnGameStart);
        }

        protected virtual void OnGameStart(object obj)
        {
            print("PPRTester - OnGameStart");
            Destroy(gameObject);
        }
    }
}