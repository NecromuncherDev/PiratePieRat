using PPR.Core;

namespace PPR.Test
{
    public class PPRTestGameStartListener : PPRMonoBehaviour
    {
        private void Start()
        {
            AddListener(PPRGameEvents.game_start_event, OnGameStart);
        }

        private void OnDestroy()
        {
            RemoveListener(PPRGameEvents.game_start_event, OnGameStart);
        }

        private void OnGameStart(object obj)
        {
            print("PPRTester - OnGameStart");
            Destroy(gameObject);
        }
    }
}