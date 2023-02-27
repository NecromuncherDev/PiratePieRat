using PPR.Core;

namespace PPR.Game
{
    public class PPRPlayer : PPRLogicMonoBehaviour
    {
        private void Start()
        {
            InvokeEvent(PPREvents.player_object_awake, gameObject);
        }
    }
}