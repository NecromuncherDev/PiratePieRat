using UnityEngine;

namespace PPR.Core
{
    public class PPRMonoManager
    {
        private PPRMonoGatewayObject monoObject;

        public PPRMonoManager()
        {
            PPRDebug.Log("PPRMonoManager");
            var temp = new GameObject("Mono Gateway");
            temp.AddComponent<PPRMonoGatewayObject>();
        }
    }

    public class PPRMonoGatewayObject : PPRMonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Manager.EventManager.InvokeEvent(PPREvents.game_pause, pauseStatus);
        }

        private void OnDestroy()
        {
            Manager.EventManager.InvokeEvent(PPREvents.game_stop_event);
        }
    }
}
