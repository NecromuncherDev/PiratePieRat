using PPR.Core;

namespace PPR.Game
{
    public class PPRPlayerShip : PPRShip 
    {
        protected override void Awake()
        {
            base.Awake();
            InvokeEvent(PPRCoreEvents.player_object_awake, ObjectID);
        }

        private void OnEnable()
        {
            AddListener(PPRCoreEvents.player_ship_move_trigger, CheckStartMoving);
            AddListener(PPRCoreEvents.player_ship_stop_trigger, CheckStopMoving);
        }

        private void OnDisable()
        {
            RemoveListener(PPRCoreEvents.player_ship_move_trigger, CheckStartMoving);
            RemoveListener(PPRCoreEvents.player_ship_stop_trigger, CheckStopMoving);
        }
    }
}