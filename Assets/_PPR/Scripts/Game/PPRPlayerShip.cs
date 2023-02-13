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
            AddListener(PPREvents.player_ship_move_trigger, CheckStartMoving);
            AddListener(PPREvents.player_ship_stop_trigger, CheckStopMoving);
        }

        private void OnDisable()
        {
            RemoveListener(PPREvents.player_ship_move_trigger, CheckStartMoving);
            RemoveListener(PPREvents.player_ship_stop_trigger, CheckStopMoving);
        }
    }
}