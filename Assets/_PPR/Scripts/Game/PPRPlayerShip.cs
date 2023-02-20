using PPR.Core;

namespace PPR.Game
{
    public class PPRPlayerShip : PPRShip 
    {
        protected override void Awake()
        {
            base.Awake();
            InvokeEvent(PPREvents.player_object_awake, ObjectID);
        }

        private void OnEnable()
        {
            AddListener(PPREvents.player_object_start_move, CheckStartMoving);
            AddListener(PPREvents.player_object_stop_move, CheckStopMoving);
        }

        private void OnDisable()
        {
            RemoveListener(PPREvents.player_object_start_move, CheckStartMoving);
            RemoveListener(PPREvents.player_object_stop_move, CheckStopMoving);
        }
    }
}