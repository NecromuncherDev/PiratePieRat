using UnityEngine;

namespace PPR.Core
{
    public class PPRMonoGateway
    {
        private PPRMonoGatewayObject monoObject;

        public PPRMonoGateway()
        {
            var temp = new GameObject("Mono Gateway");
        }
    }

    public class PPRMonoGatewayObject : PPRMonoBehaviour
    {
        private void OnApplicationPause(bool pauseStatus)
        {
            Manager.EventManager.InvokeEvent(PPREvents.game_pause, pauseStatus);
        }
    }
}
