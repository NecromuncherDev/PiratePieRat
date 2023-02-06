using System;
using UnityEngine;

namespace PPR.Core
{
    public class PPRMonoBehaviour : MonoBehaviour
    {
        protected PPRManager Manager => PPRManager.Instance;

        public void AddListener(PPRGameEvents eventName, Action<object> onEvent) => Manager.EventManager.AddListener(eventName, onEvent);
        public void RemoveListener(PPRGameEvents eventName, Action<object> onEvent) => Manager.EventManager.RemoveListener(eventName, onEvent);
        public void InvokeEvent(PPRGameEvents pprEvent, object obj) => Manager.EventManager.InvokeEvent(pprEvent, obj);
    }
}
