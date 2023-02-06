using UnityEngine;

namespace PPR.Core
{
    public class PPRMonoBehaviour : MonoBehaviour
    {
        protected PPRManager Manager => PPRManager.Instance;

        public void AddListener(PPREvent pprEvent) => Manager.EventManager.AddListener(pprEvent);
        public void RemoveListener(PPREvent pprEvent) => Manager.EventManager.RemoveListener(pprEvent);
        public void InvokeEvent(string pprEvent, object obj) => Manager.EventManager.InvokeEvent(pprEvent, obj);
    }
}
