using PPR.Core;

namespace PPR.Test
{
    public class PPRTestGameStartInvoker : PPRMonoBehaviour
    {
        public void StartGame()
        {
            Invoke("DelayStart", 0.1f);
        }
        private void DelayStart()
        {
            new PPRManager();
            InvokeEvent(GameEvents.game_start_event, null);
        }

        private void Start()
        {

            AddListener(GameEvents.game_start_event, OnGameStart);
        }

        private void OnDestroy()
        {
            RemoveListener(GameEvents.game_start_event, OnGameStart);
        }

        private void OnGameStart(object obj)
        {
            //Do something
        }
    }

    public class PPRGameUI : PPRMonoBehaviour
    {
        //Will be called from UI Button
        public void OnStartPressed()
        {
            PPRGameLogic.TryStartGame();
        }
    }

    //Will be real system
    public static class PPRGameLogic
    {
        public static bool isGameRunning = false;
        public static void TryStartGame()
        {
            if (isGameRunning)
            {
                return;
            }

            isGameRunning = true;

            PPRManager.Instance.EventManager.InvokeEvent(GameEvents.game_start_event, null);
        }
    }
}