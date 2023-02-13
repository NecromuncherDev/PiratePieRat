using PPR.Test;
using System;
using UnityEngine;

namespace PPR.Game
{
    public class PPRGameLogicLoader : PPRGameLoaderBase
    {
        [SerializeField] private PPRStrandedObjectComponent strandedOriginal;

        public override void StartLoad(Action onComplete)
        {
            var hogGameLogic = new PPRGameLogic();
            hogGameLogic.LoadManager(() =>
            {
                Manager.PoolManager.InitPool(strandedOriginal, 30, 100);

                base.StartLoad(onComplete);
            });
        }
    }
}