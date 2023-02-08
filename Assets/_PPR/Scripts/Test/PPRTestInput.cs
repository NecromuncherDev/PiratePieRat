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
            [SerializeField] internal PPRCoreEvents eventToTriggerOnKey;
            [SerializeField] internal KeyCode triggerKey;
        }

        private void Update()
        {
            foreach (KeyMapping keyMap in keyMappings)
            {
                if (Input.GetKeyDown(keyMap.triggerKey))
                {
                    InvokeEvent(keyMap.eventToTriggerOnKey, PPRGameManager.playerObjectID);
                }
            }
        }
    }
}