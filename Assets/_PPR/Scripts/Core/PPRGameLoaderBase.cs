using System;

namespace PPR.Core
{
    public class PPRGameLoaderBase : PPRMonoBehaviour
    {
        public virtual void StartLoad(Action onComplete)
        {
            onComplete.Invoke();
        }
    }
}