using System;
using System.Collections.Generic;

namespace PPR.Core
{
    public class PPREventManager
    {
        private Dictionary<PPRCoreEvents, List<Action<object>>> activeListeners = new();

        public void AddListener(PPRCoreEvents eventName, Action<object> onEvent)
        {
            if (activeListeners.TryGetValue(eventName, out var listOfEvents))
            {
                listOfEvents.Add(onEvent);
                return;
            }

            activeListeners.Add(eventName, new List<Action<object>>() { onEvent });
        }

        public void RemoveListener(PPRCoreEvents eventName, Action<object> onEvent)
        {
            if (activeListeners.TryGetValue(eventName, out var listOfEvents))
            {
                listOfEvents.Remove(onEvent);

                if (listOfEvents.Count <= 0)
                {
                    activeListeners.Remove(eventName);
                    return;
                }
            }
        }

        public void InvokeEvent(PPRCoreEvents eventName, object obj)
        {
            if (activeListeners.TryGetValue(eventName, out var listOfEvents))
            {
                foreach (var action in listOfEvents)
                {
                    action.Invoke(obj);
                }
            }
        }
    }
}
