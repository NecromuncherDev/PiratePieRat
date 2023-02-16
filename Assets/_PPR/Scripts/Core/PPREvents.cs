namespace PPR.Core
{
    public enum PPREvents
    {
        // Scene Progression
        game_start_event = 0,
        scene_loading_operation_progressed = 1,

        // Currency
        currency_set = 10,
        currency_crew_set = 11,
        currency_pies_set = 12,
        currency_cheese_set = 13,

        // Player Object
        player_object_awake = 20,
        player_ship_move_trigger = 21,
        player_ship_stop_trigger = 22,

        // Pickups and upgrades
        item_upgraded = 30,
        stranded_object_taken = 31,
    }
}