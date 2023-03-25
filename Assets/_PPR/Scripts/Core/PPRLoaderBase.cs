using System;

namespace PPR.Core
{
    public class PPRLoaderBase : PPRMonoBehaviour
    {
        public virtual void StartLoad(Action onComplete)
        {
            onComplete.Invoke();
        }
    }
}