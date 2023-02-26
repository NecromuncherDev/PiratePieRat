namespace PPR.Core
{
    public enum PPREvents
    {
        // Scene Progression 0-9
        game_start_event = 0,
        game_stop_event = 1,
        scene_loading_operation_progressed = 2,

        // Currency 10-19
        currency_set = 10,
        currency_collected = 11,
        currency_crew_set = 12,
        currency_pies_set = 13,
        currency_cheese_set = 14,

        // Player Object 20-29
        player_object_awake = 20,
        player_object_start_move = 21,
        player_object_stop_move = 22,

        // Pickups and upgrades 30-39
        item_upgraded = 30,
        pickup_taken_from_pool = 31,
        pickup_returned_to_pool = 32,
        pickup_collected = 33,
        pickup_destroyed = 34,
    }
}