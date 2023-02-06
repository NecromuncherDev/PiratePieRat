using System;

namespace PPR.Core
{
    public class PPREvent
    {
        public string eventName;
        public Action<object> eventAction;
    }
}
