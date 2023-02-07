using System;
using UnityEngine;

namespace PPR.Core
{
    public class PPRMonoBehaviour : MonoBehaviour
    {
        protected PPRManager Manager => PPRManager.Instance;
        protected string ObjectID { get; private set; }

        public void AddListener(PPRCoreEvents eventName, Action<object> onEvent) => Manager.EventManager.AddListener(eventName, onEvent);
        public void RemoveListener(PPRCoreEvents eventName, Action<object> onEvent) => Manager.EventManager.RemoveListener(eventName, onEvent);
        public void InvokeEvent(PPRCoreEvents pprEvent, object obj = null) => Manager.EventManager.InvokeEvent(pprEvent, obj);

        protected virtual void Awake()
        {
            ObjectID = $"{gameObject.name.Replace(' ', '_')}-{UnityEngine.Random.Range(0, ushort.MaxValue)}";
            print(ObjectID);
        }
    }
}