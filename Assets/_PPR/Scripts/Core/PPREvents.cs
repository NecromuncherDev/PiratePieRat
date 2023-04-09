namespace PPR.Core
{
    public enum PPREvents
    {
        // Game and Time Progression 0-9
        game_start_event = 0,
        game_stop_event = 1,
        scene_loading_operation_progressed = 2,
        game_pause = 3,
        offline_time_refreshed = 4,

        // Currency 10-19
        currency_set = 10,
        currency_collected = 11,
        currency_crew_set = 12,
        currency_pies_set = 13,
        currency_cheese_set = 14,
        currency_pies_per_second_set = 15,

        // Player and Combat 20-29
        player_object_awake = 20,
        player_object_start_move = 21,
        player_object_stop_move = 22,
        enemy_encountered = 23,
        combat_initiated = 24,
        combat_player_win = 25,
        combat_player_lose = 26,

        // Pickups and Upgrades 30-39
        item_upgraded = 30,
        pickup_taken_from_pool = 31,
        pickup_returned_to_pool = 32,
        pickup_collected = 33,
        pickup_destroyed = 34,
        pickup_created = 35,

        // Popups and Ads 40-49
        on_popup_open = 40,
        on_popup_close = 41,
        ad_show_start = 42,
        ad_show_complete = 43,
        ad_show_click = 44,

        // Purchases 50-59
        purchase_complete = 50,
    }
}