using PPR.Core;
using PPR.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PPR.Test
{
    public class PPRTestInput : PPRMonoBehaviour
    {
        [SerializeField] private List<KeyMapping> keyMappings;

        [Serializable]
        internal struct KeyMapping
        {
            [SerializeField] internal PPREvents eventToTriggerOnKey;
            [SerializeField] internal KeyCode triggerKey;
        }

        private void Update()
        {
            foreach (KeyMapping keyMap in keyMappings)
            {
                if (Input.GetKeyDown(keyMap.triggerKey))
                {
                    switch (keyMap.eventToTriggerOnKey)
                    {
                        case PPREvents.player_object_start_move:
                            InvokeEvent(keyMap.eventToTriggerOnKey, UnityEngine.Random.insideUnitCircle.normalized);
                            break;
                        
                        case PPREvents.game_start_event:
                        case PPREvents.scene_loading_operation_progressed:
                        case PPREvents.currency_set:
                        case PPREvents.currency_collected:
                        case PPREvents.currency_metal_set:
                        case PPREvents.currency_plastic_set:
                        case PPREvents.currency_wood_set:
                        case PPREvents.player_object_awake:
                        case PPREvents.player_object_stop_move:
                        case PPREvents.item_upgraded:
                        case PPREvents.pickup_taken_from_pool:
                        case PPREvents.pickup_returned_to_pool:
                        case PPREvents.pickup_collected:
                        case PPREvents.pickup_destroyed:
                        default:
                            InvokeEvent(keyMap.eventToTriggerOnKey);
                            break;
                    }
                }
            }
        }
    }
}