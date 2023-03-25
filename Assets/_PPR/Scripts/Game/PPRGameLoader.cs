using PPR.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PPR.Game
{
    public class PPRGameLoader : PPRLoaderBase
    {
        [SerializeField] private List<PPRPoolConfiguration> pools = new();

        public override void StartLoad(Action onComplete)
        {
            var pprGameLogic = new PPRGameLogic();
            pprGameLogic.LoadManager(() =>
            {
                foreach (var config in pools)
                {
                    Manager.PoolManager.InitPool(config);
                }

                base.StartLoad(onComplete);
            });
        }
    }
}