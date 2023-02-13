using System;
using PPR.Core;

namespace PPR.Game
{
    public class PPRGameLoaderBase : PPRMonoBehaviour
    {
        public virtual void StartLoad(Action onComplete)
        {
            onComplete.Invoke();
        }
    }
}