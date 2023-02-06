using System.Collections.Generic;

namespace PPR.Core
{
    public class PPREventManager
    {
        Dictionary<string, List<PPREvent>> activeListeners = new();

        public void AddListener<T>(T pprEvent) where T : PPREvent
        {
            if (activeListeners.TryGetValue(pprEvent.eventName, out var listOfEvents))
            {
                listOfEvents.Add(pprEvent);
                return;
            }

            activeListeners.Add(pprEvent.eventName, new List<PPREvent> { pprEvent });
        }

        public void RemoveListener<T>(T pprEvent) where T : PPREvent
        {
            if (activeListeners.TryGetValue(pprEvent.eventName, out var listOfEvents))
            {
                listOfEvents.Remove(pprEvent);

                if (listOfEvents.Count <= 0)
                {
                    activeListeners.Remove(pprEvent.eventName);
                    return;
                }
            }
        }

        public void InvokeEvent(string eventName, object obj)
        {
            if (activeListeners.TryGetValue(eventName, out var listOfEvents))
            {
                //TODO: Switch to for loop
                foreach (var pprEvent in listOfEvents)
                {
                    pprEvent.eventAction.Invoke(obj);
                }
            }
        }
    }
}
